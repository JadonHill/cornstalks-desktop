namespace cornstalks_app
{
    partial class CompletedOrdersForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvCompletedOrders = new System.Windows.Forms.DataGridView();
            this.lblRevenueValue = new System.Windows.Forms.Label();
            this.lblOrderCountValue = new System.Windows.Forms.Label();
            this.dgvCompletedOrderLines = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompletedOrders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompletedOrderLines)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvCompletedOrders
            // 
            this.dgvCompletedOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompletedOrders.Location = new System.Drawing.Point(12, 49);
            this.dgvCompletedOrders.Name = "dgvCompletedOrders";
            this.dgvCompletedOrders.Size = new System.Drawing.Size(1512, 389);
            this.dgvCompletedOrders.TabIndex = 0;
            // 
            // lblRevenueValue
            // 
            this.lblRevenueValue.AutoSize = true;
            this.lblRevenueValue.Location = new System.Drawing.Point(48, 21);
            this.lblRevenueValue.Name = "lblRevenueValue";
            this.lblRevenueValue.Size = new System.Drawing.Size(51, 13);
            this.lblRevenueValue.TabIndex = 2;
            this.lblRevenueValue.Text = "Revenue";
            // 
            // lblOrderCountValue
            // 
            this.lblOrderCountValue.AutoSize = true;
            this.lblOrderCountValue.Location = new System.Drawing.Point(288, 21);
            this.lblOrderCountValue.Name = "lblOrderCountValue";
            this.lblOrderCountValue.Size = new System.Drawing.Size(64, 13);
            this.lblOrderCountValue.TabIndex = 3;
            this.lblOrderCountValue.Text = "Order Count";
            // 
            // dgvCompletedOrderLines
            // 
            this.dgvCompletedOrderLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCompletedOrderLines.Location = new System.Drawing.Point(12, 241);
            this.dgvCompletedOrderLines.Name = "dgvCompletedOrderLines";
            this.dgvCompletedOrderLines.Size = new System.Drawing.Size(776, 177);
            this.dgvCompletedOrderLines.TabIndex = 1;
            // 
            // CompletedOrdersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1536, 450);
            this.Controls.Add(this.dgvCompletedOrders);
            this.Controls.Add(this.lblOrderCountValue);
            this.Controls.Add(this.lblRevenueValue);
            this.Controls.Add(this.dgvCompletedOrderLines);
            this.Name = "CompletedOrdersForm";
            this.Text = "CompletedOrdersForm";
            this.Load += new System.EventHandler(this.CompletedOrdersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompletedOrders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCompletedOrderLines)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvCompletedOrders;
        private System.Windows.Forms.Label lblRevenueValue;
        private System.Windows.Forms.Label lblOrderCountValue;
        private System.Windows.Forms.DataGridView dgvCompletedOrderLines;
    }



}