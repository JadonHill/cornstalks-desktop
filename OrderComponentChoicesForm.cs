using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace cornstalks_app
{
    public partial class OrderComponentChoicesForm : Form
    {
        private readonly int _orderId;
        private int _packageId;
        private int _componentId;
        private int _requiredQty;

        public OrderComponentChoicesForm(int orderId)
        {
            InitializeComponent();
            _orderId = orderId;

            // Wire events here (NOT in designer)
            this.Load += OrderComponentChoicesForm_Load;
            dgvComponents.CellClick += dgvComponents_CellClick;
            btnSave.Click += btnSave_Click;
        }

        private void OrderComponentChoicesForm_Load(object sender, EventArgs e)
        {
            LoadComponents();
        }

        private void LoadComponents()
        {
            SqlParameter p = new SqlParameter("@order_id", SqlDbType.Int)
            {
                Value = _orderId
            };

            DataTable dt = Db.ExecDataTable("dbo.usp_OrderComponents_GetForOrder", p);

            dgvComponents.DataSource = dt;
            dgvComponents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvComponents.ReadOnly = true;
            dgvComponents.MultiSelect = false;
            dgvComponents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            if (dgvComponents.Rows.Count > 0 && !dgvComponents.Rows[0].IsNewRow)
            {
                dgvComponents.ClearSelection();
                dgvComponents.Rows[0].Selected = true;

                DataGridViewRow r = dgvComponents.Rows[0];

                _packageId = Convert.ToInt32(r.Cells["package_id"].Value);
                _componentId = Convert.ToInt32(r.Cells["component_id"].Value);
                _requiredQty = Convert.ToInt32(r.Cells["required_total_qty"].Value);

                LoadChoices();
            }

            // Optional: hide raw IDs if you want
            // if (dgvComponents.Columns.Contains("order_id")) dgvComponents.Columns["order_id"].Visible = false;
            // if (dgvComponents.Columns.Contains("package_id")) dgvComponents.Columns["package_id"].Visible = false;
            // if (dgvComponents.Columns.Contains("component_id")) dgvComponents.Columns["component_id"].Visible = false;
        }

        private void dgvComponents_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            DataGridViewRow r = dgvComponents.Rows[e.RowIndex];

            _packageId = Convert.ToInt32(r.Cells["package_id"].Value);
            _componentId = Convert.ToInt32(r.Cells["component_id"].Value);
            _requiredQty = Convert.ToInt32(r.Cells["required_total_qty"].Value);

            LoadChoices();
        }

        private void LoadChoices()
        {
            SqlParameter[] p =
            {
                new SqlParameter("@order_id", SqlDbType.Int) { Value = _orderId },
                new SqlParameter("@package_id", SqlDbType.Int) { Value = _packageId },
                new SqlParameter("@component_id", SqlDbType.Int) { Value = _componentId }
            };

            DataTable dt = Db.ExecDataTable("dbo.usp_OrderComponentChoices_GetForComponent", p);

            dgvChoices.DataSource = dt;
            dgvChoices.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvChoices.MultiSelect = false;
            dgvChoices.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            foreach (DataGridViewColumn col in dgvChoices.Columns)
                col.ReadOnly = true;

            if (dgvChoices.Columns.Contains("chosen_qty"))
                dgvChoices.Columns["chosen_qty"].ReadOnly = false;

            // Optional: make headers nicer
            if (dgvChoices.Columns.Contains("item_name")) dgvChoices.Columns["item_name"].HeaderText = "Item";
            if (dgvChoices.Columns.Contains("chosen_qty")) dgvChoices.Columns["chosen_qty"].HeaderText = "Qty";
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_componentId == 0)
            {
                MessageBox.Show("Select a component first.");
                return;
            }

            // Build JSON from rows where qty > 0
            int total = 0;
            StringBuilder json = new StringBuilder();
            json.Append("[");

            bool first = true;

            foreach (DataGridViewRow row in dgvChoices.Rows)
            {
                if (row.IsNewRow) continue;

                int qty = 0;
                int.TryParse(row.Cells["chosen_qty"].Value?.ToString(), out qty);

                if (qty < 0)
                {
                    MessageBox.Show("Quantity cannot be negative.");
                    return;
                }

                if (qty == 0) continue;

                int itemId = Convert.ToInt32(row.Cells["item_id"].Value);

                total += qty;

                if (!first) json.Append(",");
                first = false;

                json.Append($"{{\"item_id\":{itemId},\"qty\":{qty}}}");
            }

            json.Append("]");

            if (total != _requiredQty)
            {
                MessageBox.Show($"Total must equal {_requiredQty}. You entered {total}.");
                return;
            }

            SqlParameter[] p =
            {
                new SqlParameter("@order_id", SqlDbType.Int) { Value = _orderId },
                new SqlParameter("@package_id", SqlDbType.Int) { Value = _packageId },
                new SqlParameter("@component_id", SqlDbType.Int) { Value = _componentId },
                new SqlParameter("@choices", SqlDbType.NVarChar, -1) { Value = json.ToString() }
            };

            Db.ExecNonQuery("dbo.usp_OrderComponentChoices_SaveSet", p);

            MessageBox.Show("Choices saved.");

            LoadChoices();
        }
    }
}
