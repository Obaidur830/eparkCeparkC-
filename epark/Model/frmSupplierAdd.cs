using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace epark.Model
{
    public partial class frmSupplierAdd : SampleAdd
    {
        public frmSupplierAdd()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = this;

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
                    qry = @"Insert into Supplier values (@supname, @phone, @email)";
                }
                else
                {
                    qry = @"UPDATE Supplier set supName = @supname,
                       
                        supPhone= @phone,
                        supEmail= @email
                        where supID = @id";

                }

                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@supname", txtSupName.Text);
                ht.Add("@phone", txtPhone.Text);
                ht.Add("@email", txtEmail.Text);
                if (MainClass.SQ1(qry, ht) > 0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Show("Data saved successfully");
                    id = 0;
                    txtSupName.Text = "";
                    txtPhone.Text = "";
                    txtEmail.Text = "";
                    txtSupName.Focus();
                }
            }
        }
    }
}
