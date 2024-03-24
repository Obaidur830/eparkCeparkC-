using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace epark.View
{
    public partial class loginView : Form
    {
        public loginView()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            guna2MessageDialog1.Parent = this;

        }
        bool isValidUsername = false;
        bool isValidPass = false;
        private void txtPassword_IconRightClick(object sender, EventArgs e)
        {
            
            if (txtPassword.UseSystemPasswordChar)
            {
                txtPassword.UseSystemPasswordChar = false;
                txtPassword.IconRight = Properties.Resources.blind;
               // Console.WriteLine(txtPassword.UseSystemPasswordChar);

            }
            else
            {

                txtPassword.UseSystemPasswordChar = true;
                txtPassword.IconRight = Properties.Resources.eye;

                
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //new registrationView().Show();
            this.Hide();

             new registrationView().Show();
        }

       

        private void btnLogin_Click(object sender, EventArgs e)
        {

            if( isValidUsername && isValidPass) {
                if (MainClass.isValidUser(txtUsername.Text, txtPassword.Text) == true)
                {
                    txtUsername.BorderColor = Color.Teal;
                    lbUsername.Text = "";
                    txtPassword.Text = "";
                    txtPassword.BorderColor = Color.Teal;
                    lbPassword.Text = "";
                    txtUsername.Focus();
                    isValidUsername = false;
                    isValidPass = false;
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
            else {
                if (txtUsername.Text.Trim() == "")
                {
                    txtUsername.BorderColor = Color.Red;
                    lbUsername.Text = "Please enter Username";
                    isValidUsername = false;

                }

                if (txtPassword.Text.Trim() == "")
                {
                    txtPassword.BorderColor = Color.Red;
                    lbPassword.Text = "Please enter password";
                    isValidPass = false;
                }
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                guna2MessageDialog1.Show("please remove error");
                return;
            }
            
        }

       

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                txtUsername.BorderColor = Color.Red;
                lbUsername.Text = "Please enter Username";
                isValidUsername = false;

            }
            
            else
            {
                txtUsername.BorderColor = Color.Teal;
                lbUsername.Text = "";
                isValidUsername = true;

            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            if (txtPassword.Text.Trim() == "")
            {
                txtPassword.BorderColor = Color.Red;
                lbPassword.Text = "Please enter password";
                isValidPass = false;
            }
            
            else
            {
                txtPassword.BorderColor = Color.Teal;
                lbPassword.Text = "";
                isValidPass = true;
            }
        }
    }
}
