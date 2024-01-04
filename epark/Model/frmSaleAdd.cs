using epark.View;
using Guna.UI2.WinForms;
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
    public partial class frmSaleAdd : Sample
    {
        public frmSaleAdd()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = this;

        }
        public int id = 0;
        public int cusID = 0;

        private void frmSaleAdd_Load(object sender, EventArgs e)
        {
            string qry = @"Select cusID 'id', cusName 'name' from Customer";
            MainClass.CBFill(qry, cbCustomer);
            if(cusID > 0)
            {
                cbCustomer.SelectedValue = cusID;
                loadForEdit();
                GrandTotal();
            }
            loadProductsFromDatabase();
        }

        public void AddItems(string id, string name, string price, Image pImage, string cost)
        {
            var w = new ucProduct()
            {
                pName = name,

                Price = price,
                PImage = pImage,
                pCost = cost,
                id = Convert.ToInt32(id)
            };
            flowLayoutPanel1.Controls.Add(w);
            w.onSelect += (ss, ee) =>
            {
                var wdg = (ucProduct)ss;
                foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                {
                    if (Convert.ToUInt32(item.Cells["dgvproid"].Value) == wdg.id)
                    {
                        item.Cells["dgvqty"].Value = int.Parse(item.Cells["dgvqty"].Value.ToString()) + 1;
                        item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvqty"].Value.ToString()) *
                                                        int.Parse(item.Cells["dgvPrice"].Value.ToString());
                        GrandTotal();
                        return;

                    }
                }

                guna2DataGridView1.Rows.Add(new object[] { 0, wdg.id, wdg.pName, 1, wdg.Price, wdg.Price, wdg.pCost });
                GrandTotal();
            };
        }
        private void GrandTotal()
        {
            double tot = 0;
            lbTotal.Text = "";
            foreach(DataGridViewRow item in guna2DataGridView1.Rows)
            {
                tot += double.Parse(item.Cells["dgvAmount"].Value.ToString());
            }
            lbTotal.Text = tot.ToString("N2");
        }  

        private void loadProductsFromDatabase()
        {
            string qry = @"Select * from Product";
            SqlCommand cmd = new SqlCommand(qry, MainClass.sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach(DataRow row in dt.Rows)
                {
                    Byte[] imageArray = (byte[])row["pImage"];
                    byte[] imageByteArray = imageArray;
                    AddItems(row["proID"].ToString(), row["pName"].ToString(), row["pPrice"].ToString(),
                        Image.FromStream(new MemoryStream(imageArray)), row["pCost"].ToString());
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            guna2DataGridView1.Rows.Clear();
            txtDate.Value = DateTime.Now;
           // cbCustomer.SelectedIndex = 0;
            cbCustomer.SelectedIndex = -1;
            lbTotal.Text = "0.0";
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            foreach(var item in flowLayoutPanel1.Controls)
            {
                var pro = (ucProduct)item;
                pro.Visible = pro.pName.ToLower().Contains(txtSearch.Text.Trim().ToLower());
            }
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                string qry = "Select * from Product where pBarcode like '" + txtBarcode.Text + "'";
                SqlCommand cmd = new SqlCommand(qry, MainClass.sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    foreach (DataGridViewRow item in guna2DataGridView1.Rows)
                        
                    {
                        
                        if (Convert.ToUInt32(item.Cells["dgvproid"].Value) == int.Parse(row["proID"].ToString()))
                        {
                            item.Cells["dgvqty"].Value = int.Parse(item.Cells["dgvqty"].Value.ToString()) + 1;
                            item.Cells["dgvAmount"].Value = int.Parse(item.Cells["dgvqty"].Value.ToString()) *
                                                            int.Parse(item.Cells["dgvPrice"].Value.ToString());
                            GrandTotal();
                            txtBarcode.Text = "";
                            return;

                        }
                    }
                    guna2DataGridView1.Rows.Add(new object[] { 0, row["proID"].ToString(),
                                                                  row["pName"].ToString(),1 ,
                                                                  row["pPrice"].ToString(),
                                                                  row["pPrice"].ToString(),
                                                                  row["pCost"].ToString()});
                    txtBarcode.Text = "";

                }
            }
        }

        private void btmSave_Click(object sender, EventArgs e)
        {
            if (MainClass.validation(this) == false)
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                guna2MessageDialog1.Show("please fill sale form correctly");
                return;
            }
            string qry1 = "";
            string qry2 = "";
            int record = 0;
            if (id == 0)
            {
                qry1 = @"Insert into tblMain Values(@date,@type, @supID);
                          Select SCOPE_IDENTITY()";
            }
            else
            {
                qry1 = @"UPDATE tblMain set mdate= @date, mType=@type, mSupCusID=@supID
                      where MainID= @id";

            }
            SqlCommand cmd1 = new SqlCommand(qry1, MainClass.sqlCon);
            cmd1.Parameters.AddWithValue("@id", id);
            cmd1.Parameters.AddWithValue("@date", Convert.ToDateTime(txtDate.Value).Date);
            cmd1.Parameters.AddWithValue("@type", "SAL");
            cmd1.Parameters.AddWithValue("@supID", Convert.ToInt32(cbCustomer.SelectedValue));

            if (MainClass.sqlCon.State == ConnectionState.Closed) { MainClass.sqlCon.Open(); }
            if (id == 0)
            {
                id = Convert.ToInt32(cmd1.ExecuteScalar());
            }
            else
            {
                cmd1.ExecuteNonQuery();

            }
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                int did = Convert.ToInt32(row.Cells["dgvid"].Value);
                if (did == 0)
                {
                    qry2 = @"Insert into tblDetails Values(@mID,@proID,@qty,@price,@amount,@cost)";

                }
                else
                {
                    qry2 = @"Update tblDetails set dMainID = @mID,productID = @proID,
                          qty = @qty, price = @price, amount = @amount, cost = @cost
                          where detailID = @id";
                }
                try
                {
                    SqlCommand cmd2 = new SqlCommand(qry2, MainClass.sqlCon);
                    cmd2.Parameters.AddWithValue("@id", did);
                    cmd2.Parameters.AddWithValue("@mID", id);
                    cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproid"].Value));
                    cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvqty"].Value));
                    cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvCost"].Value));
                    cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));
                    cmd2.Parameters.AddWithValue("@cost", Convert.ToDouble(row.Cells["dgvCost"].Value));
                    record += cmd2.ExecuteNonQuery();
                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    MainClass.sqlCon.Close();
                }


            }
            if (MainClass.sqlCon.State == ConnectionState.Open) { MainClass.sqlCon.Close(); }

            if (record > 0)

            {

                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                guna2MessageDialog1.Show("Saved Successfully");
                id = 0;
                cusID = 0;
                txtDate.Value = DateTime.Now;
                cbCustomer.SelectedIndex = -1;
                guna2DataGridView1.Rows.Clear();
                lbTotal.Text = "0.0";
            }

        }
        private void loadForEdit()
        {
            string qry = "Select * from tblDetails inner join Product on proID = productID  where dMainID= " + id + " ";
            SqlCommand cmd = new SqlCommand(qry, MainClass.sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                string did;
                string pid;
                string pname;
                string qty;
                string cost;
                string amt;
                did = row["detailID"].ToString();
                pname = row["pName"].ToString();
                pid = row["proID"].ToString();
                qty = row["qty"].ToString();
                cost = row["price"].ToString();
                amt = row["amount"].ToString();
                amt = row["cost"].ToString();

               guna2DataGridView1.Rows.Add( did, pid, pname, qty, cost, amt, cost);
            }
        }

        private void guna2DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvDel")
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;

                if (guna2MessageDialog1.Show("are you really want to delete?") == DialogResult.Yes)
                {
                    int rowIndex = guna2DataGridView1.CurrentCell.RowIndex;
                    guna2DataGridView1.Rows.RemoveAt(rowIndex);
                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qry = "Delete from tblMain where mainID = " + id + "";
                    string qry1 = "Delete from tblDetails where dMainID = " + id + "";

                    Hashtable ht = new Hashtable();
                    MainClass.SQ1(qry, ht);
                    if (MainClass.SQ1(qry1, ht) > 0)
                    {
                        /*guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                        guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                        guna2MessageDialog1.Show("Deleted Successfully");*/
                        
                    }
                    GrandTotal();
                }

            }
        }

        private void guna2PictureBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(picTicketBox, "print ticket");
        }
        ticketView ticketView = new ticketView();
        // Guna2DataGridView guna2DataGridView2 = new Guna2DataGridView();
        private void picTicketBox_Click(object sender, EventArgs e)
        {
          // ticketView.guna2DataGridView2.Rows.Clear();
            DataTable dt = new DataTable();
            dt.Columns.Add("Product", typeof(string));
            dt.Columns.Add("Qty", typeof(int));
            dt.Columns.Add("Price", typeof(double));
            dt.Columns.Add("Amount", typeof(double));
            foreach (DataGridViewRow item in guna2DataGridView1.Rows)
            {
                dt.Rows.Add(item.Cells["dgvProduct"].Value.ToString(), int.Parse(item.Cells["dgvqty"].Value.ToString()), int.Parse(item.Cells["dgvPrice"].Value.ToString()), int.Parse(item.Cells["dgvAmount"].Value.ToString()));
                
            }
            ticketView.guna2DataGridView2.DataSource= dt;
            //Console.WriteLine(guna2DataGridView2.Rows.Count);
            //ticketView.ShowDialog();*/
           PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument1;
            printPreviewDialog.ShowDialog();
        }

        Rectangle MyBox_Rectangle;
        Rectangle MyTest_Rectangle;
        SizeF Size_MyColums;
        int niopp = 0;
        int numm = 0;
        private int[] MyCoulums_Width = { 200, 200, 200, 200 };
        private StringAlignment[] Vertical_Ali = { StringAlignment.Center, StringAlignment.Center, StringAlignment.Center, StringAlignment.Center };
        private StringAlignment[] Horezontal_Ali = { StringAlignment.Center, StringAlignment.Center, StringAlignment.Center, StringAlignment.Far };
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            {
                Graphics MyGraphics = e.Graphics;
                Font MyFont0 = new Font("Arial", 24, FontStyle.Bold);
                String MyLine = "____________________________________";
                int Horezontal_X = 0;
                int vertical_Y = 0;
                int My_newline = 0;
                My_newline += 0;
                My_newline = My_newline + 20;
                var g = e.Graphics;
                var srcRect = new Rectangle(0, 0, picTicketBox.Width, picTicketBox.Height);
                var desRect = new Rectangle(0, 0, 850, 1100);
                using (var MYPic = new Bitmap(srcRect.Width, srcRect.Height))
                {
                    picTicketBox.DrawToBitmap(MYPic, srcRect);
                    g.DrawImage(MYPic, desRect, srcRect, GraphicsUnit.Pixel);

                }
                My_newline = My_newline + 60;
                StringFormat MyStringFormat2 = new StringFormat();
                StringFormat MyStringFormat3 = new StringFormat();
                SolidBrush My_Color = new SolidBrush(Color.Black);
                MyStringFormat3 = new StringFormat(StringFormatFlags.DirectionVertical);
              //  MyStringFormat2 = new StringFormat(StringFormatFlags.DirectionHori);
                /*MyGraphics.DrawString(label1.Text.ToUpper(), new Font("Arial", 23, FontStyle.Bold), My_Color, 15, vertical_Y + 12);
                My_newline = My_newline + 60;
                MyGraphics.DrawString(cashier + textBox1.Text, MyFont0, My_Color, 205, vertical_Y + My_newline, MyStringFormat2);
                My_newline = My_newline + 25;
                MyGraphics.DrawString(bill + textBox2.Text, MyFont0, My_Color, 85, vertical_Y + My_newline);
                My_newline = My_newline + 25;
                g.DrawString(datee + DateTime.Now.ToShortDateString(), MyFont0, My_Color, 75, vertical_Y + My_newline);
                My_newline = My_newline + 10;*/
               // My_newline = My_newline + 10;
               // MyGraphics.DrawString(MyLine, new Font("Arial", 10, FontStyle.Bold), My_Color, 0, vertical_Y + My_newline);
                My_newline = My_newline + 25;
                MyGraphics.DrawString("Product", new Font("Arial", 24, FontStyle.Bold), My_Color, Horezontal_X + 2, vertical_Y + My_newline, MyStringFormat2);
                MyGraphics.DrawString("Qty", new Font("Arial", 24, FontStyle.Bold), My_Color, Horezontal_X + 220, vertical_Y + My_newline);
                MyGraphics.DrawString("Price", new Font("Arial", 24, FontStyle.Bold), My_Color, Horezontal_X + 420, vertical_Y + My_newline);
                MyGraphics.DrawString("Amount", new Font("Arial", 24, FontStyle.Bold), My_Color, 620, vertical_Y + My_newline);
                My_newline = My_newline + 60;
                 

                //----------------------------------------------myDataGrid------------------------------------
                StringFormat MyStringFormat = new StringFormat();
                Font MyFont1 = new Font("Arial", 24);
                const int Side_margin = 4;
                int y = My_newline;
                for (int j = numm; j < ticketView.guna2DataGridView2.Rows.Count; j++)
                {
                    int MyMax_height = 0;
                    niopp++;

                    if (niopp <= 10)
                    {
                        numm++;
                        if (numm < ticketView.guna2DataGridView2.Rows.Count)
                        {

                            for (int i = 0; i < ticketView.guna2DataGridView2.Columns.Count; i++)
                            {
                                Size_MyColums = e.Graphics.MeasureString(Convert.ToString(ticketView.guna2DataGridView2.Rows[j].Cells[i].Value), MyFont1, MyCoulums_Width[i] - 1 * Side_margin);
                                int MyNew_height = (int)Math.Ceiling(Size_MyColums.Height);
                                if (MyMax_height < MyNew_height)
                                {
                                    MyMax_height = MyNew_height;
                                }
                            }
                            MyMax_height += 2 * Side_margin;

                            int x = Side_margin;
                            for (int i = 0; i < ticketView.guna2DataGridView2.Columns.Count; i++)
                            {
                                MyBox_Rectangle = new Rectangle(x, y, MyCoulums_Width[i], MyMax_height);
                                MyTest_Rectangle = MyBox_Rectangle;
                                MyTest_Rectangle.Inflate(-Side_margin, -Side_margin);
                                MyStringFormat.Alignment = Horezontal_Ali[i];
                                MyStringFormat.LineAlignment = Vertical_Ali[i];
                                e.Graphics.DrawString(Convert.ToString(ticketView.guna2DataGridView2.Rows[j].Cells[i].Value), MyFont1, My_Color, MyTest_Rectangle, MyStringFormat);
                               // box akare thakbe gridview er moto
                                // e.Graphics.DrawRectangle(Pens.Black, MyBox_Rectangle);
                                x += MyCoulums_Width[i];
                            }
                        }
                        else
                        {
                            e.HasMorePages = false;
                        }
                    }
                    else
                    {
                        niopp = 0;
                        e.HasMorePages = true;
                        return;
                    }
                    y += MyMax_height;
                }
                //----------------------------------------------myDataGrid------------------------------------
                /*My_newline = y + 10;
                textBox5.Text = String.Format("{0:n0}", double.Parse(textBox5.Text));
                textBox6.Text = String.Format("{0:n0}", double.Parse(textBox6.Text));
                MyGraphics.DrawString(textBox5.Text, MyFont0, My_Color, 0, vertical_Y + My_newline);
                MyGraphics.DrawString("كۆی گشتی:", MyFont0, My_Color, 280, vertical_Y + My_newline, MyStringFormat2);
                My_newline = My_newline + 18;
                MyGraphics.DrawString("***************************************************", new Font("Arial", 10), My_Color, 0, vertical_Y + My_newline);
                My_newline = My_newline + 20;
                MyGraphics.DrawString(textBox6.Text, MyFont0, My_Color, 2, vertical_Y + My_newline);
                MyGraphics.DrawString("داشكاندن:", MyFont0, My_Color, 280, vertical_Y + My_newline, MyStringFormat2);
                My_newline = My_newline + 18;
                MyGraphics.DrawString("***************************************************", new Font("Arial", 10), My_Color, 0, vertical_Y + My_newline);
                My_newline = My_newline + 30;
                MyGraphics.DrawString("*" + textBox2.Text + "*", new Font("C39P24DlTt", 25), My_Color, 35, vertical_Y + My_newline);
                My_newline = My_newline + 35;
                MyGraphics.DrawString(textBox2.Text, new Font("Arial", 8), My_Color, 115, vertical_Y + My_newline);
                My_newline = My_newline + 40;
                MyGraphics.DrawString(textBox4.Text, MyFont0, My_Color, 40, vertical_Y + My_newline);
                My_newline = My_newline + 20;
                MyGraphics.DrawString(Auther, MyFont0, My_Color, 10, vertical_Y + My_newline);*/

                niopp = 0;
                numm = 0;
            }
        }
    }
}
