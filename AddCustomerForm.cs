using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cornstalks_app
{
    public partial class AddCustomerForm : Form
    {
        public AddCustomerForm()
        {
            InitializeComponent();

            btnSave.Click += btnSave_Click;
            btnCancel.Click += btnCancel_Click;

            // -----------------------------
            // Default the DOB picker to something sensible
            // (Optional)
            // -----------------------------
            dtpDOB.Value = new DateTime(2000, 1, 1);
        }

        /// <summary>
        /// SAVE BUTTON:
        /// 1) Validate user input
        /// 2) Build SQL parameters
        /// 3) Call dbo.usp_Customers_Insert
        /// 4) If it works: DialogResult = OK (tells Form1 to refresh customers)
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // ------------------------------------------------------------
                // 1) READ INPUTS FROM THE FORM
                // ------------------------------------------------------------
                string first = txtFirstName.Text.Trim();
                string last = txtLastName.Text.Trim();
                string phone = txtPhone.Text.Trim();
                string email = txtEmail.Text.Trim();

                // date picker gives us a DateTime - we only care about the DATE part
                DateTime dob = dtpDOB.Value.Date;

                // Optional fields can be blank -> send NULL to SQL
                string street = txtStreet.Text.Trim();
                string city = txtCity.Text.Trim();
                string state = txtState.Text.Trim();
                string zip = txtZip.Text.Trim();
                string payment = txtPaymentInfo.Text.Trim();

                // BIT field: checkbox checked = 1, unchecked = 0
                bool discount = chkDiscount.Checked;

                // ------------------------------------------------------------
                // 2) BASIC VALIDATION (keep this simple for now)
                // ------------------------------------------------------------

                // Required fields
                if (string.IsNullOrWhiteSpace(first))
                {
                    MessageBox.Show("First name is required.");
                    txtFirstName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(last))
                {
                    MessageBox.Show("Last name is required.");
                    txtLastName.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(phone))
                {
                    MessageBox.Show("Phone is required.");
                    txtPhone.Focus();
                    return;
                }

                // Your DB column is VARCHAR(10) (no dashes)
                // So we should enforce exactly 10 digits
                if (phone.Length != 10 || !long.TryParse(phone, out _))
                {
                    MessageBox.Show("Phone must be exactly 10 digits (example: 5551234567).");
                    txtPhone.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(email))
                {
                    MessageBox.Show("Email is required.");
                    txtEmail.Focus();
                    return;
                }

                // Zip in your DB is VARCHAR(5) - allow blank, but if provided it must be 5 digits
                if (!string.IsNullOrWhiteSpace(zip))
                {
                    if (zip.Length != 5 || !int.TryParse(zip, out _))
                    {
                        MessageBox.Show("Zip must be 5 digits.");
                        txtZip.Focus();
                        return;
                    }
                }

                // ------------------------------------------------------------
                // 3) BUILD SQL PARAMETERS FOR dbo.usp_Customers_Insert
                //    IMPORTANT:
                //    - parameter names MUST match the stored procedure
                //    - optional blanks should be sent as DBNull.Value (SQL NULL)
                // ------------------------------------------------------------

                SqlParameter pFirst = new SqlParameter("@first_name", SqlDbType.VarChar, 20) { Value = first };
                SqlParameter pLast = new SqlParameter("@last_name", SqlDbType.VarChar, 20) { Value = last };
                SqlParameter pPhone = new SqlParameter("@phone", SqlDbType.VarChar, 10) { Value = phone };
                SqlParameter pEmail = new SqlParameter("@email", SqlDbType.VarChar, 50) { Value = email };
                SqlParameter pDob = new SqlParameter("@DateofBirth", SqlDbType.Date) { Value = dob };

                // Helper: convert "" to NULL for SQL
                object streetValue = string.IsNullOrWhiteSpace(street) ? (object)DBNull.Value : street;
                object cityValue = string.IsNullOrWhiteSpace(city) ? (object)DBNull.Value : city;
                object stateValue = string.IsNullOrWhiteSpace(state) ? (object)DBNull.Value : state;
                object zipValue = string.IsNullOrWhiteSpace(zip) ? (object)DBNull.Value : zip;
                object paymentValue = string.IsNullOrWhiteSpace(payment) ? (object)DBNull.Value : payment;

                SqlParameter pStreet = new SqlParameter("@street_address", SqlDbType.VarChar, 40) { Value = streetValue };
                SqlParameter pCity = new SqlParameter("@city", SqlDbType.VarChar, 20) { Value = cityValue };
                SqlParameter pState = new SqlParameter("@state", SqlDbType.VarChar, 20) { Value = stateValue };
                SqlParameter pZip = new SqlParameter("@zip", SqlDbType.VarChar, 5) { Value = zipValue };
                SqlParameter pPayment = new SqlParameter("@payment_info", SqlDbType.VarChar, 30) { Value = paymentValue };

                // discount is BIT in SQL
                SqlParameter pDiscount = new SqlParameter("@discount", SqlDbType.Bit) { Value = discount };

                // ------------------------------------------------------------
                // 4) RUN THE STORED PROCEDURE
                // ------------------------------------------------------------
                Db.ExecNonQuery(
                    "dbo.usp_Customers_Insert",
                    pFirst, pLast, pPhone, pEmail, pDob,
                    pStreet, pCity, pState, pZip, pPayment, pDiscount
                );

                // ------------------------------------------------------------
                // 5) IF WE MADE IT HERE, SQL INSERT WORKED
                //    Tell Form1: "it succeeded"
                // ------------------------------------------------------------
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (SqlException ex)
            {
                // SQL errors land here (duplicate email, etc.)

                MessageBox.Show("SQL error:\n\n" + ex.Message);
            }
            catch (Exception ex)
            {
                // Any other error
                MessageBox.Show("Unexpected error:\n\n" + ex.Message);
            }
        }

        /// <summary>
        /// CANCEL BUTTON:
        /// just close the form and tell Form1: "no changes"
        /// </summary>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void AddCustomerForm_Load(object sender, EventArgs e)
        {
            // Optional: set default DOB
            dtpDOB.Value = new DateTime(2000, 1, 1);
        }

    }
}
