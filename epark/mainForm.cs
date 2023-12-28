using epark.View;
using Guna.UI2.WinForms;
using System;
using System.Windows.Forms;

namespace epark
{
    public partial class mainForm : Form
    {
        static mainForm _frmMainObj;
        public static mainForm Instance
        {
            get { if (_frmMainObj == null) { _frmMainObj = new mainForm(); } return _frmMainObj; }
        }
        public mainForm()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = this;
        }


        private void mainForm_Load_1(object sender, EventArgs e)
        {
            _frmMainObj = this;
            btnMax.PerformClick();
            lbUser.Text = MainClass.USER;
            guna2CirclePictureBox1.Image = MainClass.IMG;
            btnHome.PerformClick();

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {

        }
        public void addControls(Form f)
        {
            this.centerPanel.Controls.Clear();
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            centerPanel.Controls.Add(f);
            f.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            addControls(new frmUserView());
        }

        private void btnCategory_Click(object sender, EventArgs e)
        {
            addControls(new frmCategoryView());
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            addControls(new frmSupplierView());
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            addControls(new frmCustomerView());
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            addControls(new frmProductView());
        }

        private void btnPurchase_Click(object sender, EventArgs e)
        {
            addControls(new frmPurchase());
        }

        private void btnSale_Click(object sender, EventArgs e)
        {
            addControls(new frmSalesView());
        }

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
            guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;

            if (guna2MessageDialog1.Show("are you really want to exit Application ?") == DialogResult.No)
            {
                e.Cancel = true;
            }
        }

        private void mainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            addControls(new frmDashboard());
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            addControls(new frmReports());
        }
    }
}
