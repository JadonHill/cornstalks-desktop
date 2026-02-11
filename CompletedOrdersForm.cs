using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cornstalks_app
{
    public partial class CompletedOrdersForm : Form
    {
        public CompletedOrdersForm()
        {
            InitializeComponent();

            // If your designer already wires this up, this is optional.
            // dgvCompletedOrders.CellClick += dgvCompletedOrders_CellClick;
        }

        private void CompletedOrdersForm_Load(object sender, EventArgs e)
        {
            LoadCompletedOrders();
            LoadRevenueAllTime();
        }

        private void LoadCompletedOrders()
        {
            DataTable dt = Db.ExecDataTable("dbo.usp_Orders_GetCompleted");
            dgvCompletedOrders.DataSource = dt;

            dgvCompletedOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCompletedOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCompletedOrders.MultiSelect = false;
            dgvCompletedOrders.ReadOnly = true;
        }

        private void LoadRevenueAllTime()
        {
            DataTable dt = Db.ExecDataTable("dbo.usp_Revenue_GetAllTime");

            decimal revenue = 0m;
            int count = 0;

            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["total_revenue"] != DBNull.Value)
                    revenue = Convert.ToDecimal(dt.Rows[0]["total_revenue"]);

                if (dt.Rows[0]["completed_order_count"] != DBNull.Value)
                    count = Convert.ToInt32(dt.Rows[0]["completed_order_count"]);
            }

            lblRevenueValue.Text = revenue.ToString("C");
            lblOrderCountValue.Text = count.ToString();
        }

        private void dgvCompletedOrders_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int orderId = Convert.ToInt32(dgvCompletedOrders.Rows[e.RowIndex].Cells["order_id"].Value);

            SqlParameter p = new SqlParameter("@order_id", SqlDbType.Int) { Value = orderId };
            DataTable dtLines = Db.ExecDataTable("dbo.usp_OrderPackages_GetByOrderId", p);
            dgvCompletedOrderLines.DataSource = dtLines;

            dgvCompletedOrderLines.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvCompletedOrderLines.ReadOnly = true;
        }

        private void btnRestoreOrder_Click(object sender, EventArgs e)
        {
            if (dgvCompletedOrders.CurrentRow == null)
            {
                MessageBox.Show("Select an order to restore.");
                return;
            }

            int orderId = Convert.ToInt32(dgvCompletedOrders.CurrentRow.Cells["order_id"].Value);

            var confirm = MessageBox.Show(
                $"Restore order #{orderId} back to Current Orders?",
                "Restore Order",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm != DialogResult.Yes) return;

            SqlParameter pOrderId = new SqlParameter("@order_id", SqlDbType.Int) { Value = orderId };
            Db.ExecNonQuery("dbo.usp_Orders_SetNotCompleted", pOrderId);

            LoadCompletedOrders();
            LoadRevenueAllTime();              // ✅ update revenue + count
            dgvCompletedOrderLines.DataSource = null;
        }
    }
}
