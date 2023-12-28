namespace epark.Model
{
    partial class ucProduct
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.guna2ShadowPanel1 = new Guna.UI2.WinForms.Guna2ShadowPanel();
            this.lbPrice = new System.Windows.Forms.Label();
            this.lbPName = new System.Windows.Forms.Label();
            this.ucProductbox = new Guna.UI2.WinForms.Guna2PictureBox();
            this.guna2ShadowPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ucProductbox)).BeginInit();
            this.SuspendLayout();
            // 
            // guna2ShadowPanel1
            // 
            this.guna2ShadowPanel1.BackColor = System.Drawing.Color.Transparent;
            this.guna2ShadowPanel1.Controls.Add(this.lbPrice);
            this.guna2ShadowPanel1.Controls.Add(this.lbPName);
            this.guna2ShadowPanel1.FillColor = System.Drawing.Color.White;
            this.guna2ShadowPanel1.Location = new System.Drawing.Point(11, 50);
            this.guna2ShadowPanel1.Name = "guna2ShadowPanel1";
            this.guna2ShadowPanel1.ShadowColor = System.Drawing.Color.Black;
            this.guna2ShadowPanel1.Size = new System.Drawing.Size(126, 140);
            this.guna2ShadowPanel1.TabIndex = 0;
            // 
            // lbPrice
            // 
            this.lbPrice.Font = new System.Drawing.Font("Arial Narrow", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPrice.Location = new System.Drawing.Point(7, 92);
            this.lbPrice.Name = "lbPrice";
            this.lbPrice.Size = new System.Drawing.Size(116, 23);
            this.lbPrice.TabIndex = 1;
            this.lbPrice.Text = "200";
            this.lbPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbPName
            // 
            this.lbPName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbPName.Location = new System.Drawing.Point(3, 43);
            this.lbPName.Name = "lbPName";
            this.lbPName.Size = new System.Drawing.Size(120, 49);
            this.lbPName.TabIndex = 0;
            this.lbPName.Text = "Product Name";
            this.lbPName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ucProductbox
            // 
            this.ucProductbox.BackColor = System.Drawing.Color.Transparent;
            this.ucProductbox.Image = global::epark.Properties.Resources.cubes;
            this.ucProductbox.ImageRotate = 0F;
            this.ucProductbox.Location = new System.Drawing.Point(30, 0);
            this.ucProductbox.Name = "ucProductbox";
            this.ucProductbox.Size = new System.Drawing.Size(90, 90);
            this.ucProductbox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ucProductbox.TabIndex = 1;
            this.ucProductbox.TabStop = false;
            this.ucProductbox.UseTransparentBackground = true;
            this.ucProductbox.Click += new System.EventHandler(this.ucProductbox_Click);
            // 
            // ucProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.ucProductbox);
            this.Controls.Add(this.guna2ShadowPanel1);
            this.Name = "ucProduct";
            this.Size = new System.Drawing.Size(153, 196);
            this.guna2ShadowPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ucProductbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2ShadowPanel guna2ShadowPanel1;
        private Guna.UI2.WinForms.Guna2PictureBox ucProductbox;
        private System.Windows.Forms.Label lbPrice;
        private System.Windows.Forms.Label lbPName;
    }
}
