using System;
using System.Windows.Forms;

namespace epark
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void gunaPictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

            /*  if (MainClass.isValidUser(txtUserName.Text, txtPassword.Text) == false)
              {
                  guna2MessageDialog2.Show("invalid username or password");
                  return;
              }
              else
              {
                  this.Hide();
                  mainForm frmMain = new mainForm();
                  frmMain.Show();
              }*/
            mainForm frmMain = new mainForm();
            frmMain.Show();

        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
