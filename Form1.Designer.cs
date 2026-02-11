namespace cornstalks_app
{
    partial class Form1
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
            this.cmbCustomers = new System.Windows.Forms.ComboBox();
            this.dgvPackages = new System.Windows.Forms.DataGridView();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.dtpCreatedDate = new System.Windows.Forms.DateTimePicker();
            this.btnCreateOrder = new System.Windows.Forms.Button();
            this.lblStatus = new System.Windows.Forms.Label();
            this.dgvOrderLines = new System.Windows.Forms.DataGridView();
            this.dgvOrders = new System.Windows.Forms.DataGridView();
            this.btnAddCustomer = new System.Windows.Forms.Button();
            this.btnCompleteOrder = new System.Windows.Forms.Button();
            this.btnViewCompletedOrders = new System.Windows.Forms.Button();
            this.btnPickList = new System.Windows.Forms.Button();
            this.btnComponentChoices = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderLines)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbCustomers
            // 
            this.cmbCustomers.FormattingEnabled = true;
            this.cmbCustomers.Location = new System.Drawing.Point(31, 207);
            this.cmbCustomers.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbCustomers.Name = "cmbCustomers";
            this.cmbCustomers.Size = new System.Drawing.Size(160, 24);
            this.cmbCustomers.TabIndex = 0;
            this.cmbCustomers.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // dgvPackages
            // 
            this.dgvPackages.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPackages.Location = new System.Drawing.Point(16, 15);
            this.dgvPackages.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvPackages.Name = "dgvPackages";
            this.dgvPackages.RowHeadersWidth = 51;
            this.dgvPackages.Size = new System.Drawing.Size(1736, 185);
            this.dgvPackages.TabIndex = 1;
            this.dgvPackages.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvPackages_CellContentClick);
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(255, 210);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(132, 22);
            this.txtNotes.TabIndex = 2;
            // 
            // dtpCreatedDate
            // 
            this.dtpCreatedDate.Location = new System.Drawing.Point(396, 210);
            this.dtpCreatedDate.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dtpCreatedDate.Name = "dtpCreatedDate";
            this.dtpCreatedDate.Size = new System.Drawing.Size(265, 22);
            this.dtpCreatedDate.TabIndex = 3;
            // 
            // btnCreateOrder
            // 
            this.btnCreateOrder.Location = new System.Drawing.Point(671, 212);
            this.btnCreateOrder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCreateOrder.Name = "btnCreateOrder";
            this.btnCreateOrder.Size = new System.Drawing.Size(100, 28);
            this.btnCreateOrder.TabIndex = 4;
            this.btnCreateOrder.Text = "Create Order";
            this.btnCreateOrder.UseVisualStyleBackColor = true;
            this.btnCreateOrder.Click += new System.EventHandler(this.btnCreateOrder_Click);
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Location = new System.Drawing.Point(200, 210);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(44, 16);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "label1";
            // 
            // dgvOrderLines
            // 
            this.dgvOrderLines.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrderLines.Location = new System.Drawing.Point(16, 439);
            this.dgvOrderLines.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvOrderLines.Name = "dgvOrderLines";
            this.dgvOrderLines.RowHeadersWidth = 51;
            this.dgvOrderLines.Size = new System.Drawing.Size(1736, 185);
            this.dgvOrderLines.TabIndex = 6;
            // 
            // dgvOrders
            // 
            this.dgvOrders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOrders.Location = new System.Drawing.Point(16, 247);
            this.dgvOrders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.dgvOrders.Name = "dgvOrders";
            this.dgvOrders.RowHeadersWidth = 51;
            this.dgvOrders.Size = new System.Drawing.Size(1736, 185);
            this.dgvOrders.TabIndex = 7;
            this.dgvOrders.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvOrders_CellContentClick);
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.Location = new System.Drawing.Point(779, 212);
            this.btnAddCustomer.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(124, 28);
            this.btnAddCustomer.TabIndex = 8;
            this.btnAddCustomer.Text = "Add Customer";
            this.btnAddCustomer.UseVisualStyleBackColor = true;
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // btnCompleteOrder
            // 
            this.btnCompleteOrder.Location = new System.Drawing.Point(551, 631);
            this.btnCompleteOrder.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCompleteOrder.Name = "btnCompleteOrder";
            this.btnCompleteOrder.Size = new System.Drawing.Size(157, 28);
            this.btnCompleteOrder.TabIndex = 9;
            this.btnCompleteOrder.Text = "Complete Order";
            this.btnCompleteOrder.UseVisualStyleBackColor = true;
            this.btnCompleteOrder.Click += new System.EventHandler(this.btnCompleteOrder_Click);
            // 
            // btnViewCompletedOrders
            // 
            this.btnViewCompletedOrders.Location = new System.Drawing.Point(733, 631);
            this.btnViewCompletedOrders.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnViewCompletedOrders.Name = "btnViewCompletedOrders";
            this.btnViewCompletedOrders.Size = new System.Drawing.Size(183, 28);
            this.btnViewCompletedOrders.TabIndex = 10;
            this.btnViewCompletedOrders.Text = "View Completed Orders";
            this.btnViewCompletedOrders.UseVisualStyleBackColor = true;
            this.btnViewCompletedOrders.Click += new System.EventHandler(this.btnViewCompletedOrders_Click_1);
            // 
            // btnPickList
            // 
            this.btnPickList.Location = new System.Drawing.Point(275, 631);
            this.btnPickList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnPickList.Name = "btnPickList";
            this.btnPickList.Size = new System.Drawing.Size(231, 28);
            this.btnPickList.TabIndex = 11;
            this.btnPickList.Text = "Current Orders Picklist";
            this.btnPickList.UseVisualStyleBackColor = true;
            this.btnPickList.Click += new System.EventHandler(this.btnPickList_Click);
            // 
            // btnComponentChoices
            // 
            this.btnComponentChoices.Location = new System.Drawing.Point(923, 215);
            this.btnComponentChoices.Name = "btnComponentChoices";
            this.btnComponentChoices.Size = new System.Drawing.Size(174, 23);
            this.btnComponentChoices.TabIndex = 12;
            this.btnComponentChoices.Text = "Choose Mum Colors";
            this.btnComponentChoices.UseVisualStyleBackColor = true;
            this.btnComponentChoices.Click += new System.EventHandler(this.btnComponentChoices_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1833, 725);
            this.Controls.Add(this.btnComponentChoices);
            this.Controls.Add(this.btnPickList);
            this.Controls.Add(this.btnViewCompletedOrders);
            this.Controls.Add(this.btnCompleteOrder);
            this.Controls.Add(this.btnAddCustomer);
            this.Controls.Add(this.dgvOrders);
            this.Controls.Add(this.dgvOrderLines);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.btnCreateOrder);
            this.Controls.Add(this.dtpCreatedDate);
            this.Controls.Add(this.txtNotes);
            this.Controls.Add(this.dgvPackages);
            this.Controls.Add(this.cmbCustomers);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPackages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrderLines)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOrders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbCustomers;
        private System.Windows.Forms.DataGridView dgvPackages;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.DateTimePicker dtpCreatedDate;
        private System.Windows.Forms.Button btnCreateOrder;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.DataGridView dgvOrderLines;
        private System.Windows.Forms.DataGridView dgvOrders;
        private System.Windows.Forms.Button btnAddCustomer;
        private System.Windows.Forms.Button btnCompleteOrder;
        private System.Windows.Forms.Button btnViewCompletedOrders;
        private System.Windows.Forms.Button btnPickList;
        private System.Windows.Forms.Button btnComponentChoices;
    }
}

