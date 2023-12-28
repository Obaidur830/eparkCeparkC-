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
    }
}
