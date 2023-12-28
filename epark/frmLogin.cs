using System;
using System.Windows.Forms;

namespace epark
{
    public partial class frmLogin : Sample
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

              if (MainClass.isValidUser(txtUserName.Text, txtPassword.Text) == true)
              {
                  this.Hide();
                  mainForm frmMain = new mainForm();
                  frmMain.Show();
                  
              }
              else
              {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                guna2MessageDialog1.Show("Invalid username or password");
                 //  guna2MessageDialog2.Show("invalid username or password");
                  //return;
                  
              }
            
        }

        private void txtUserName_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
