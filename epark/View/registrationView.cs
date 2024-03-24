using epark.AltBox;
using Guna.UI2.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace epark.View
{
    public partial class registrationView : Form
    {
        public registrationView()
        {
            InitializeComponent();
            txtPassword.UseSystemPasswordChar = true;
            guna2MessageDialog1.Parent = this;

        }
        bool isValidUsername=false;
        bool isValidPhone = false;
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

        

        private void btnRegister_Click(object sender, EventArgs e)
        {
            
            if(isValidUsername && isValidPhone && isValidPass)
            {
                string qry = @"Insert into users values (@username,@pass, @phone, @image)";

                Byte[] imageByteArray;
                Image temp = new Bitmap(epark.Properties.Resources.user_01);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageByteArray = ms.ToArray();
                Hashtable ht = new Hashtable();
                ht.Add("@id", 0);
                ht.Add("@username", txtUsername.Text);
                ht.Add("@pass", txtPassword.Text);
                ht.Add("@phone", txtPhone.Text);
                ht.Add("@image", imageByteArray);
                if (MainClass.SQ1(qry, ht) > 0)
                {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Saved Successfully");

                    txtUsername.Text = "";
                    txtUsername.BorderColor = Color.Teal;
                    lbUsername.Text = "";
                    txtPhone.Text = "";
                    txtPhone.BorderColor = Color.Teal;
                    lbPhone.Text = "";
                    txtPassword.Text = "";
                    txtPassword.BorderColor = Color.Teal;
                    lbPassword.Text = "";

                    txtUsername.Focus();
                    isValidUsername = false;
                    isValidPhone = false;
                    isValidPass = false;
                }
                else {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                    guna2MessageDialog1.Show("error while inserting resistration data");
                    return;
                }
                
            }
            else
            {
                if (txtUsername.Text.Trim() == "")
                {
                    txtUsername.BorderColor = Color.Red;
                    lbUsername.Text = "Please enter Username";
                    isValidUsername = false;

                }
                if (txtPhone.Text.Trim() == "")
                {
                    txtPhone.BorderColor = Color.Red;
                    lbPhone.Text = "Please enter Phone Number";
                    isValidPhone = false;

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
            
           
       
        

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            if (txtUsername.Text.Trim() == "")
            {
                txtUsername.BorderColor = Color.Red;
                lbUsername.Text = "Please enter Username";
                isValidUsername = false;

            }
            else if (!MainClass.isValidUsername(txtUsername.Text))
            {
                txtUsername.BorderColor = Color.Red;
                lbUsername.Text = "Username already exists";
                isValidUsername = false;

            }
            else
            {
                txtUsername.BorderColor = Color.Teal;
                lbUsername.Text = "";
                isValidUsername = true;

            }
        }

        private void txtMobile_TextChanged(object sender, EventArgs e)
        {
            if (txtPhone.Text.Trim() == "")
            {
                txtPhone.BorderColor = Color.Red;
                lbPhone.Text = "Please enter Phone Number";
                isValidPhone = false;

            }
            else if (!MainClass.isValidPhoneNumber(txtPhone.Text))
            {
                txtPhone.BorderColor = Color.Red;
                lbPhone.Text = "Please enter a valid phone no.";
                isValidPhone = false;

            }
            else
            {
                txtPhone.BorderColor = Color.Teal;
                lbPhone.Text = "";
                isValidPhone = true;
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
            else if (!MainClass.isValidPassword(txtPassword.Text))
            {
                txtPassword.BorderColor = Color.Red;
                lbPassword.Text = "Password at least 5 character long";
                isValidPass = false;
            }
            else
            {
                txtPassword.BorderColor = Color.Teal;
                lbPassword.Text = "";
                isValidPass = true;
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //new loginView().Show();
            this.Hide();
            
           new loginView().Show();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
