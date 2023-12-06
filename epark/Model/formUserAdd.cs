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
using System.IO;

namespace epark.Model
{
    public partial class formUserAdd : SampleAdd
    {
        public formUserAdd()
        {
            InitializeComponent();
        }

        private void formUserAdd_Load(object sender, EventArgs e)
        {

        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

        }

        int id = 0;
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
                    qry = @"Insert into users values (@username,@pass, @phone, @image)";
                }
                else
                {
                    qry = @"UPDATE users set uUsername = @username,
                        uPass= @pass,
                        uPhone= @phone,
                        uImage= @image
                        where userID = @id";

                }
                Image temp = new Bitmap(uPicBox.Image);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageByteArray = ms.ToArray();

                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@username", txtUserName.Text);
                ht.Add("@pass", txtPass.Text);
                ht.Add("@phone", txtPhone.Text);
                ht.Add("@image", imageByteArray);
                if(MainClass.SQ1(qry, ht)>0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Show("Data saved successfully");
                    id = 0;
                    txtUserName.Text = "";
                    txtPass.Text = "";
                    txtPhone.Text = "";
                    uPicBox.Image = epark.Properties.Resources.user_01;
                    txtUserName.Focus();
                }
            }
        }

        public string filePath = "";
        Byte[] imageByteArray;
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png) | *.png, *.jpg ";
            if(ofd.ShowDialog()==DialogResult.OK)
            {
                filePath = ofd.FileName;
                uPicBox.Image = new Bitmap(filePath);
            }


        }
    }
}
