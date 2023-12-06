using epark.View;
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
        }


        private void mainForm_Load_1(object sender, EventArgs e)
        {
            _frmMainObj = this;
            btnMax.PerformClick();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            f.Dock = DockStyle.Fill;
            f.TopLevel = false;
            centerPanel.Controls.Add(f);
            f.Show();
        }

        private void btnUsers_Click(object sender, EventArgs e)
        {
            addControls(new frmUserView());
        }
    }
}
