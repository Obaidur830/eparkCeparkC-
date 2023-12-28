using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace epark.Model
{
    public partial class frmProductAdd : SampleAdd
    {
        public frmProductAdd()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = this;

        }
        public int id = 0;
        public int catID = 0;
        private void frmProductAdd_Load(object sender, EventArgs e)
        {
            string qry = "Select catID 'id', catName 'name' from Category";

            MainClass.CBFill(qry, pCategoryCombo);
            if (id > 0)
            {
                pCategoryCombo.SelectedValue = catID;
                LoadImage();
            }
        }

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
                    qry = @"Insert into Product values (@name,@pCatID, @barcode, @cost,@saleprice, @image)";
                }
                else
                {
                    qry = @"UPDATE Product set pName = @name,
                        pCatID= @pCatID,
                        pBarcode= @barcode,
                        pCost= @cost,
                        pPrice= @saleprice,
                        pImage=@image
                        where proID = @id";

                }
                Image temp = new Bitmap(pPicBox.Image);
                MemoryStream ms = new MemoryStream();
                temp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                imageByteArray = ms.ToArray();

                Hashtable ht = new Hashtable();
                ht.Add("@id", id);
                ht.Add("@name", txtPName.Text);
                ht.Add("@pCatID", Convert.ToInt32(pCategoryCombo.SelectedValue));
                ht.Add("@barcode", txtBarcode.Text);
                ht.Add("@cost", Convert.ToDouble(txtCost.Text) );
                ht.Add("@saleprice", Convert.ToDouble(txtSalePrice.Text));
                ht.Add("@image", imageByteArray);
                if (MainClass.SQ1(qry, ht) > 0)
                {
                    guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                    guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                    guna2MessageDialog1.Show("Data saved successfully");
                    id = 0;
                    txtPName.Text = "";
                    txtBarcode.Text = "";
                    pCategoryCombo.SelectedIndex= -1;
                    txtCost.Text = "";
                    txtSalePrice.Text = "";
                    pPicBox.Image = epark.Properties.Resources.offer;
                    txtPName.Focus();
                }
            }
        }

        public string filePath = "";
        Byte[] imageByteArray;
       
        private void LoadImage()
        {
            string qry = @"Select pImage from Product where proID = " + id + "";
            SqlCommand cmd = new SqlCommand(qry, MainClass.sqlCon);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Byte[] imageArray = (byte[])dt.Rows[0]["pImage"];
                byte[] imageByteArray = imageArray;
                pPicBox.Image = Image.FromStream(new MemoryStream(imageByteArray));
            }
        }

        private void btnBrowse_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Images(.jpg, .png) | *.png; *.jpg ";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                filePath = ofd.FileName;
                pPicBox.Image = new Bitmap(filePath);
            }
        }
    }
}
  
   