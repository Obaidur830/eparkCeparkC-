namespace epark
{
    partial class SampleView
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
            this.sampleViewPanel = new Guna.UI2.WinForms.Guna2Panel();
            this.btnAdd = new Guna.UI2.WinForms.Guna2Button();
            this.txtBoxSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbSearch = new System.Windows.Forms.Label();
            this.lbSampleHeader = new System.Windows.Forms.Label();
            this.sampleViewPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // sampleViewPanel
            // 
            this.sampleViewPanel.AutoScroll = true;
            this.sampleViewPanel.Controls.Add(this.btnAdd);
            this.sampleViewPanel.Controls.Add(this.txtBoxSearch);
            this.sampleViewPanel.Controls.Add(this.lbSearch);
            this.sampleViewPanel.Controls.Add(this.lbSampleHeader);
            this.sampleViewPanel.CustomBorderColor = System.Drawing.Color.Transparent;
            this.sampleViewPanel.CustomBorderThickness = new System.Windows.Forms.Padding(5);
            this.sampleViewPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.sampleViewPanel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sampleViewPanel.Location = new System.Drawing.Point(0, 0);
            this.sampleViewPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sampleViewPanel.Name = "sampleViewPanel";
            this.sampleViewPanel.Size = new System.Drawing.Size(686, 97);
            this.sampleViewPanel.TabIndex = 0;
            // 
            // btnAdd
            // 
            this.btnAdd.Animated = true;
            this.btnAdd.AutoRoundedCorners = true;
            this.btnAdd.BorderRadius = 16;
            this.btnAdd.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAdd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAdd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAdd.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(95)))), ((int)(((byte)(61)))), ((int)(((byte)(204)))));
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.Image = global::epark.Properties.Resources.add;
            this.btnAdd.Location = new System.Drawing.Point(17, 46);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 34);
            this.btnAdd.TabIndex = 3;
            this.btnAdd.Text = "Add New";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // txtBoxSearch
            // 
            this.txtBoxSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBoxSearch.AutoRoundedCorners = true;
            this.txtBoxSearch.BorderRadius = 13;
            this.txtBoxSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBoxSearch.DefaultText = "";
            this.txtBoxSearch.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtBoxSearch.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtBoxSearch.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxSearch.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtBoxSearch.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBoxSearch.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtBoxSearch.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtBoxSearch.IconLeft = global::epark.Properties.Resources.search;
            this.txtBoxSearch.Location = new System.Drawing.Point(424, 45);
            this.txtBoxSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBoxSearch.Name = "txtBoxSearch";
            this.txtBoxSearch.PasswordChar = '\0';
            this.txtBoxSearch.PlaceholderText = "Search Here";
            this.txtBoxSearch.SelectedText = "";
            this.txtBoxSearch.Size = new System.Drawing.Size(147, 28);
            this.txtBoxSearch.TabIndex = 2;
            this.txtBoxSearch.TextOffset = new System.Drawing.Point(5, 0);
            this.txtBoxSearch.TextChanged += new System.EventHandler(this.txtBoxSearch_TextChanged);
            // 
            // lbSearch
            // 
            this.lbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbSearch.Location = new System.Drawing.Point(424, 20);
            this.lbSearch.Name = "lbSearch";
            this.lbSearch.Size = new System.Drawing.Size(47, 17);
            this.lbSearch.TabIndex = 1;
            this.lbSearch.Text = "Search";
            // 
            // lbSampleHeader
            // 
            this.lbSampleHeader.AutoSize = true;
            this.lbSampleHeader.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbSampleHeader.Location = new System.Drawing.Point(12, 12);
            this.lbSampleHeader.Name = "lbSampleHeader";
            this.lbSampleHeader.Size = new System.Drawing.Size(140, 25);
            this.lbSampleHeader.TabIndex = 0;
            this.lbSampleHeader.Text = "Sample Header";
            // 
            // SampleView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 370);
            this.Controls.Add(this.sampleViewPanel);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "SampleView";
            this.Text = "SampleView";
            this.Load += new System.EventHandler(this.SampleView_Load);
            this.sampleViewPanel.ResumeLayout(false);
            this.sampleViewPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public Guna.UI2.WinForms.Guna2Button btnAdd;
        public System.Windows.Forms.Label lbSampleHeader;
        public System.Windows.Forms.Label lbSearch;
        public Guna.UI2.WinForms.Guna2TextBox txtBoxSearch;
        public Guna.UI2.WinForms.Guna2Panel sampleViewPanel;
    }
}