using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace cornstalks_app
{
    public partial class PickListForm : Form
    {
        private bool _loading = false;

        public PickListForm()
        {
            InitializeComponent();

            // Wire events here so it always works even if the Designer events get weird
            this.Load += PickListForm_Load;
            // btnRefresh.Click += btnRefresh_Click;
            btnSave.Click += btnSave_Click;
            // btnReset.Click += btnReset_Click;
        }

        private void PickListForm_Load(object sender, EventArgs e)
        {
            dgvPickList.DataError += (s, args) => { args.ThrowException = false; };

            // Commit edits reliably (important for typing then immediately clicking Save)
            dgvPickList.CurrentCellDirtyStateChanged += (s, e2) =>
            {
                if (dgvPickList.IsCurrentCellDirty)
                    dgvPickList.CommitEdit(DataGridViewDataErrorContexts.Commit);
            };

            // Update Remaining after editing Picked
            dgvPickList.CellEndEdit += dgvPickList_CellEndEdit;

            LoadPickList();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadPickList();
        }

        private void LoadPickList()
        {
            _loading = true;

            DataTable dt = Db.ExecDataTable("dbo.usp_PickList_GetCurrentTotals");
            dgvPickList.DataSource = dt;

            dgvPickList.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvPickList.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPickList.MultiSelect = false;
            dgvPickList.AllowUserToAddRows = false;

            // Hide item_id but keep it for saving
            if (dgvPickList.Columns.Contains("item_id"))
                dgvPickList.Columns["item_id"].Visible = false;

            // Headers
            if (dgvPickList.Columns.Contains("item_name"))
                dgvPickList.Columns["item_name"].HeaderText = "Item";
            if (dgvPickList.Columns.Contains("total_qty"))
                dgvPickList.Columns["total_qty"].HeaderText = "Total Needed";
            if (dgvPickList.Columns.Contains("picked_qty"))
                dgvPickList.Columns["picked_qty"].HeaderText = "Picked";
            if (dgvPickList.Columns.Contains("remaining_qty"))
                dgvPickList.Columns["remaining_qty"].HeaderText = "Remaining";

            // Read-only everything except picked_qty
            foreach (DataGridViewColumn col in dgvPickList.Columns)
                col.ReadOnly = true;

            if (dgvPickList.Columns.Contains("picked_qty"))
                dgvPickList.Columns["picked_qty"].ReadOnly = false;

            _loading = false;
        }

        // Live UI math only (does not save to SQL)
        private void dgvPickList_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (_loading) return;
            if (e.RowIndex < 0) return;

            if (dgvPickList.Columns[e.ColumnIndex].Name != "picked_qty")
                return;

            DataGridViewRow row = dgvPickList.Rows[e.RowIndex];

            int total = ToInt(row.Cells["total_qty"].Value);
            int picked = ToInt(row.Cells["picked_qty"].Value);

            // Clamp
            if (picked < 0) picked = 0;
            if (picked > total) picked = total;

            // Only write back if needed (prevents event weirdness)
            if (ToInt(row.Cells["picked_qty"].Value) != picked)
                row.Cells["picked_qty"].Value = picked;

            row.Cells["remaining_qty"].Value = total - picked;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                // Force current edit to commit into the DataTable
                dgvPickList.EndEdit();
                dgvPickList.CommitEdit(DataGridViewDataErrorContexts.Commit);
                dgvPickList.CurrentCell = null;

                if (!(dgvPickList.DataSource is DataTable dt))
                {
                    MessageBox.Show("Grid is not bound to a DataTable.");
                    return;
                }

                foreach (DataRow r in dt.Rows)
                {
                    int itemId = ToInt(r["item_id"]);
                    int pickedQty = ToInt(r["picked_qty"]);

                    // extra safety
                    if (itemId <= 0) continue;
                    if (pickedQty < 0) pickedQty = 0;

                    SqlParameter pItem = new SqlParameter("@item_id", SqlDbType.Int) { Value = itemId };
                    SqlParameter pPicked = new SqlParameter("@picked_qty", SqlDbType.Int) { Value = pickedQty };

                    Db.ExecNonQuery("dbo.usp_PickProgress_SetPickedQty", pItem, pPicked);
                }

                LoadPickList();
                MessageBox.Show("Pick progress saved.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "SAVE FAILED");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            var confirm = MessageBox.Show(
                "Reset ALL picked progress?",
                "Reset",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes) return;

            try
            {
                Db.ExecNonQuery("dbo.usp_PickProgress_Reset");
                LoadPickList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "RESET FAILED");
            }
        }

        private static int ToInt(object value)
        {
            if (value == null) return 0;
            int x;
            return int.TryParse(value.ToString(), out x) ? x : 0;
        }
    }
}
