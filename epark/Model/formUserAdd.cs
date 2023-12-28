using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace epark.Model
{
    public partial class formUserAdd : SampleAdd
    {
        public formUserAdd()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = this;

        }

        private void formUserAdd_Load(object sender, EventArgs e)
        {
            if (id > 0)
            {
                LoadImage();
            }
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
                if (MainClass.SQ1(qry, ht) > 0)
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
            ofd.Filter = "Images(.jpg, .png) | *.png; *.jpg ";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                uPicBox.Image = new Bitmap(filePath);
            }


        }
        private void LoadImage()
        {
            string qry = @"Select uImage from users where userID = " + id + "";
            SqlCommand cmd = new SqlCommand(qry, MainClass.sqlCon);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["uImage"] != DBNull.Value)
                {
                    Byte[] imageArray = (byte[])dt.Rows[0]["uImage"];
                    byte[] imageByteArray = imageArray;
                    uPicBox.Image = Image.FromStream(new MemoryStream(imageByteArray));

                }
               /* Byte[] imageArray = (byte[])dt.Rows[0]["uImage"];
                byte[] imageByteArray = imageArray;
                uPicBox.Image = Image.FromStream(new MemoryStream(imageByteArray));*/
            }
        }
    }
}
