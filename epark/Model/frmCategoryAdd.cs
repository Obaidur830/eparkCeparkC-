using System.Collections;
using System.Drawing;
using System.IO;
using System;

namespace epark.Model
{
    public partial class frmCategoryAdd : SampleAdd
    {
        public frmCategoryAdd()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = this;
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
                    qry = @"Insert into Category values (@name)";
                }
                else
                {
                    qry = @"UPDATE Category set catName = @name
                        
                        where catID = @id";

                }
                

                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@name", txtCatName.Text);
               
                if (MainClass.SQ1(qry, ht) > 0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Show("Data saved successfully");
                    id = 0;
                    txtCatName.Text = "";
                    txtCatName.Focus();
                }
            }
        }
    }
}
