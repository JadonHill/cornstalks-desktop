namespace cornstalks_app
{
    partial class OrderComponentChoicesForm
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
            this.btnSave = new System.Windows.Forms.Button();
            this.dgvChoices = new System.Windows.Forms.DataGridView();
            this.dgvComponents = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvChoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponents)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(348, 338);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 0;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // dgvChoices
            // 
            this.dgvChoices.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvChoices.Location = new System.Drawing.Point(12, 182);
            this.dgvChoices.Name = "dgvChoices";
            this.dgvChoices.RowHeadersWidth = 51;
            this.dgvChoices.RowTemplate.Height = 24;
            this.dgvChoices.Size = new System.Drawing.Size(776, 150);
            this.dgvChoices.TabIndex = 1;
            // 
            // dgvComponents
            // 
            this.dgvComponents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvComponents.Location = new System.Drawing.Point(12, 16);
            this.dgvComponents.Name = "dgvComponents";
            this.dgvComponents.RowHeadersWidth = 51;
            this.dgvComponents.RowTemplate.Height = 24;
            this.dgvComponents.Size = new System.Drawing.Size(776, 150);
            this.dgvComponents.TabIndex = 2;
            // 
            // OrderComponentChoicesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dgvComponents);
            this.Controls.Add(this.dgvChoices);
            this.Controls.Add(this.btnSave);
            this.Name = "OrderComponentChoicesForm";
            this.Text = "OrderComponentChoicesForm";
            this.Load += new System.EventHandler(this.OrderComponentChoicesForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvChoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvComponents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.DataGridView dgvChoices;
        private System.Windows.Forms.DataGridView dgvComponents;
    }
}