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

namespace epark.Model
{
    public partial class frmCustomerAdd : SampleAdd
    {
        public frmCustomerAdd()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = this;

        }
        private void frmCustomerAdd_Load(object sender, EventArgs e)
        {

        }
        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }
        public int id = 0;
        public override void btmSave_Click(object sender, EventArgs e)
        {
            if (MainClass.validation(this) == false)
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                guna2MessageDialog1.Show("please remove error");
                return;
            }
            else
            {
                string qry = "";
                if (id == 0)
                {
                    qry = @"Insert into Customer values (@cusname, @phone, @email)";
                }
                else
                {
                    qry = @"UPDATE Customer set cusName = @cusname,
                       
                        cusPhone= @phone,
                        cusEmail= @email
                        where cusID = @id";

                }
               
                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@cusname", txtCusName.Text);
                ht.Add("@phone", txtPhone.Text);
                ht.Add("@email", txtEmail.Text);
                if (MainClass.SQ1(qry, ht) > 0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Show("Data saved successfully");
                    id = 0;
                    txtCusName.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";
                    txtCusName.Focus();
                }
            }
        }
    }
}
