namespace epark.Model
{
    partial class frmCategoryAdd
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
            this.txtCatName = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbCatName = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Size = new System.Drawing.Size(127, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Category Add";
            // 
            // txtCatName
            // 
            this.txtCatName.AutoRoundedCorners = true;
            this.txtCatName.BorderRadius = 13;
            this.txtCatName.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCatName.DefaultText = "";
            this.txtCatName.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCatName.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCatName.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCatName.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCatName.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(61)))), ((int)(((byte)(204)))));
            this.txtCatName.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCatName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.txtCatName.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(61)))), ((int)(((byte)(204)))));
            this.txtCatName.Location = new System.Drawing.Point(29, 154);
            this.txtCatName.Name = "txtCatName";
            this.txtCatName.PasswordChar = '\0';
            this.txtCatName.PlaceholderText = "";
            this.txtCatName.SelectedText = "";
            this.txtCatName.Size = new System.Drawing.Size(181, 29);
            this.txtCatName.TabIndex = 0;
            this.txtCatName.Tag = "v";
            this.txtCatName.TextOffset = new System.Drawing.Point(10, 0);
            // 
            // lbCatName
            // 
            this.lbCatName.AutoSize = true;
            this.lbCatName.Location = new System.Drawing.Point(35, 134);
            this.lbCatName.Name = "lbCatName";
            this.lbCatName.Size = new System.Drawing.Size(100, 17);
            this.lbCatName.TabIndex = 1;
            this.lbCatName.Text = "Category Name";
            // 
            // frmCategoryAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 285);
            this.Controls.Add(this.txtCatName);
            this.Controls.Add(this.lbCatName);
            this.Name = "frmCategoryAdd";
            this.Text = "frmCategoryAdd";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public Guna.UI2.WinForms.Guna2TextBox txtCatName;
        private System.Windows.Forms.Label lbCatName;
    }
}