using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cornstalks_app
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            /*
            DataTable dt = Db.ExecDataTable("dbo.usp_Customers_GetActive");

            MessageBox.Show("Customers loaded: " + dt.Rows.Count);
            */ // that was a test to show that 9 customers were loaded from sql
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadCustomersIntoCombo();

            cmbCustomers.DropDownStyle = ComboBoxStyle.DropDown; // allow typing

            cmbCustomers.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cmbCustomers.AutoCompleteSource = AutoCompleteSource.ListItems;


            dtpCreatedDate.Value = DateTime.Today;
            LoadPackagesIntoGrid();
            LoadOrdersIntoGrid();
        }




        private void LoadPackagesIntoGrid()
        {
            // Pull packages from SQL
            DataTable dtPackages = Db.ExecDataTable("dbo.usp_Packages_GetAvailable");

            // Show them in the grid
            dgvPackages.DataSource = dtPackages;

            // Make the grid look nice
            dgvPackages.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Lock all DB columns so user can’t edit package names/prices
            foreach (DataGridViewColumn col in dgvPackages.Columns)
                col.ReadOnly = true;

            // Add Quantity column once (user types how many they want)
            if (!dgvPackages.Columns.Contains("Quantity"))
            {
                DataGridViewTextBoxColumn qtyCol = new DataGridViewTextBoxColumn();
                qtyCol.Name = "Quantity";
                qtyCol.HeaderText = "Quantity";
                qtyCol.ReadOnly = false; // this is the only editable column
                dgvPackages.Columns.Add(qtyCol);
            }

            // Default quantity = 0 for every row
            foreach (DataGridViewRow row in dgvPackages.Rows)
            {
                if (!row.IsNewRow)
                    row.Cells["Quantity"].Value = 0;
            }

            // Hide package_id from the user (still usable in code)
            if (dgvPackages.Columns.Contains("package_id"))
                dgvPackages.Columns["package_id"].Visible = false;
        }


        private void btnCreateOrder_Click(object sender, EventArgs e)
        {
            try
            {
                // ------------------------------------------------------------
                // 1) BASIC FORM VALUES (customer/date/notes)
                // ------------------------------------------------------------

                if (cmbCustomers.SelectedValue == null)
                {
                    MessageBox.Show("Please select a customer.");
                    return;
                }

                int customerId = Convert.ToInt32(cmbCustomers.SelectedValue);
                DateTime createdDate = dtpCreatedDate.Value.Date;

                string notes = txtNotes.Text.Trim();
                object notesValue = string.IsNullOrWhiteSpace(notes) ? (object)DBNull.Value : notes;

                // ------------------------------------------------------------
                // 2) BUILD THE TVP ("@lines") FROM dgvPackages
                //    SQL expects dbo.OrderPackageLineType:
                //      package_id INT
                //      quantity   INT
                // ------------------------------------------------------------

                DataTable cart = new DataTable();

                // IMPORTANT: column names must match the SQL TVP type
                cart.Columns.Add("package_id", typeof(int));
                cart.Columns.Add("quantity", typeof(int));

                foreach (DataGridViewRow row in dgvPackages.Rows)
                {
                    if (row.IsNewRow) continue;

                    // package_id column comes from the packages query (hidden is fine)
                    int packageId = Convert.ToInt32(row.Cells["package_id"].Value);

                    // Quantity column is the UI column you added
                    object qtyObj = row.Cells["Quantity"].Value;

                    int qty = 0;
                    if (qtyObj != null && int.TryParse(qtyObj.ToString(), out int parsedQty))
                        qty = parsedQty;

                    if (qty > 0)
                    {
                        cart.Rows.Add(packageId, qty);
                    }
                }

                if (cart.Rows.Count == 0)
                {
                    MessageBox.Show("Enter a Quantity > 0 for at least one package.");
                    return;
                }

                // ------------------------------------------------------------
                // 3) PARAMETERS FOR dbo.usp_Orders_CreateWithPackages
                // ------------------------------------------------------------

                SqlParameter pCustomer = new SqlParameter("@customer_id", SqlDbType.Int) { Value = customerId };
                SqlParameter pCreated = new SqlParameter("@created_date", SqlDbType.Date) { Value = createdDate };
                SqlParameter pNotes = new SqlParameter("@notes", SqlDbType.VarChar, 200) { Value = notesValue };

                // TVP parameter
                SqlParameter pLines = new SqlParameter("@lines", SqlDbType.Structured);
                pLines.TypeName = "dbo.OrderPackageLineType"; // EXACT SQL type name
                pLines.Value = cart;

                // OUTPUT parameter
                SqlParameter pNewId = new SqlParameter("@new_order_id", SqlDbType.Int);
                pNewId.Direction = ParameterDirection.Output;

                // ------------------------------------------------------------
                // 4) CALL THE REAL STORED PROCEDURE (with packages)
                // ------------------------------------------------------------

                Db.ExecNonQuery(
                    "dbo.usp_Orders_CreateWithPackages",
                    pCustomer, pCreated, pNotes, pLines, pNewId
                );

                int newOrderId = Convert.ToInt32(pNewId.Value);

                // ------------------------------------------------------------
                // 5) SUCCESS UI
                // ------------------------------------------------------------

                MessageBox.Show($"Order #{newOrderId} created!");

                LoadOrdersIntoGrid();

                foreach (DataGridViewRow r in dgvOrders.Rows)
                {
                    if (!r.IsNewRow && Convert.ToInt32(r.Cells["order_id"].Value) == newOrderId)
                    {
                        dgvOrders.ClearSelection();
                        r.Selected = true;
                        dgvOrders.FirstDisplayedScrollingRowIndex = r.Index;
                        break;
                    }
                }


                // Reset quantities after order is created
                foreach (DataGridViewRow row in dgvPackages.Rows)
                {
                    if (!row.IsNewRow)
                        row.Cells["Quantity"].Value = 0;
                }

                txtNotes.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error creating order:\n\n" + ex.Message);
            }
        }


        /// <summary>
        /// Loads customers from SQL Server and binds them to the DataGridView.
        /// It respects the "Show inactive too" checkbox.
        /// </summary>
        /// 



        private void LoadOrdersIntoGrid()
        {
            // Pull the order "headers" (one row per order)
            DataTable dtOrders = Db.ExecDataTable("dbo.usp_Orders_GetRecent");

            dgvOrders.DataSource = dtOrders;

            // Make it readable
            dgvOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrders.MultiSelect = false;
            dgvOrders.ReadOnly = true;

            // Optional: nicer column headers
            if (dgvOrders.Columns.Contains("order_id")) dgvOrders.Columns["order_id"].HeaderText = "Order #";
            if (dgvOrders.Columns.Contains("created_date")) dgvOrders.Columns["created_date"].HeaderText = "Created";
            if (dgvOrders.Columns.Contains("total_price")) dgvOrders.Columns["total_price"].HeaderText = "Total";
        }

        private void dgvOrders_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // If they clicked the column header, ignore
            if (e.RowIndex < 0) return;

            // Get the order_id from the clicked row
            int orderId = Convert.ToInt32(dgvOrders.Rows[e.RowIndex].Cells["order_id"].Value);

            LoadOrderLines(orderId);
        }


        private void LoadOrderLines(int orderId)
        {
            // Create parameter for stored procedure
            SqlParameter pOrderId = new SqlParameter("@order_id", SqlDbType.Int);
            pOrderId.Value = orderId;

            // Pull package lines for that order
            DataTable dtLines = Db.ExecDataTable("dbo.usp_OrderPackages_GetByOrderId", pOrderId);

            dgvOrderLines.DataSource = dtLines;

            // Make it readable
            dgvOrderLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvOrderLines.ReadOnly = true;
            dgvOrderLines.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvOrderLines.MultiSelect = false;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            // 1) Create the AddCustomerForm (your popup form)
            using (AddCustomerForm frm = new AddCustomerForm())
            {
                // 2) Show it as a MODAL dialog (user must close it before going back)
                DialogResult result = frm.ShowDialog();

                // 3) If they clicked Save and it succeeded, the form should set DialogResult.OK
                if (result == DialogResult.OK)
                {
                    // 4) Refresh the customers dropdown (so the new person appears)
                    LoadCustomersIntoCombo();

                    MessageBox.Show("Customer added!");
                }
            }

        }


        private void LoadCustomersIntoCombo()
        {
            DataTable dtCustomers = Db.ExecDataTable("dbo.usp_Customers_GetActive");

            // add full_name column if needed
            if (!dtCustomers.Columns.Contains("full_name"))
            {
                dtCustomers.Columns.Add("full_name", typeof(string));
            }

            foreach (DataRow row in dtCustomers.Rows)
            {
                string first = row["first_name"].ToString();
                string last = row["last_name"].ToString();

                row["full_name"] = $"{first}, {last}";
            }

            cmbCustomers.DisplayMember = "full_name";
            cmbCustomers.ValueMember = "customer_id";
            cmbCustomers.DataSource = dtCustomers;
        }


        private void btnViewCompletedOrders_Click(object sender, EventArgs e)
        {
            using (CompletedOrdersForm frm = new CompletedOrdersForm())
            {
                frm.ShowDialog();
            }

            // When they close it, refresh current orders (in case they restored one)
            LoadOrdersIntoGrid();
        }


        private void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow == null)
            {
                MessageBox.Show("Please select an order.");
                return;
            }

            int orderId = Convert.ToInt32(dgvOrders.CurrentRow.Cells["order_id"].Value);

            var confirm = MessageBox.Show(
                $"Complete order #{orderId}?",
                "Complete Order",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes)
                return;

            SqlParameter pOrderId = new SqlParameter("@order_id", SqlDbType.Int) { Value = orderId };

            Db.ExecNonQuery("dbo.usp_Orders_SetCompleted", pOrderId);

            MessageBox.Show($"Order #{orderId} marked complete.");

            LoadOrdersIntoGrid();
            dgvOrderLines.DataSource = null; // clear bottom grid
        }

        private void btnViewCompletedOrders_Click_1(object sender, EventArgs e)
        {
            btnViewCompletedOrders_Click(sender, e);
        }

        private void btnPickList_Click(object sender, EventArgs e)
        {
            using (PickListForm frm = new PickListForm())
            {
                frm.ShowDialog();
            }
        }

        private void btnComponentChoices_Click(object sender, EventArgs e)
        {
            if (dgvOrders.CurrentRow == null)
            {
                MessageBox.Show("Please select an order.");
                return;
            }

            int orderId =
                Convert.ToInt32(dgvOrders.CurrentRow.Cells["order_id"].Value);

            using (var frm = new OrderComponentChoicesForm(orderId))
            {
                frm.ShowDialog();
            }
        }

        private void dgvPackages_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

    }






}

