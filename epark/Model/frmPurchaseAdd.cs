using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace epark.Model
{
    public partial class frmPurchaseAdd : SampleAdd
    {
        public frmPurchaseAdd()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = this;

        }
        public int mainID = 0;
        public int supID = 0;
       
        private void frmPurchaseAdd_Load(object sender, EventArgs e)
        {
            cbProduct.SelectedIndexChanged -= new EventHandler(cbProduct_SelectedIndexChanged);
            string qry = "Select proID 'id', pName 'name' from Product";
            string qry2 = "Select supID 'id', supName 'name' from Supplier";
            MainClass.CBFill(qry, cbProduct);
            MainClass.CBFill(qry2, cbSupplier);

            if(supID > 0)
            {
                cbSupplier.SelectedValue = supID;
                loadForEdit();
            }
            cbProduct.SelectedIndexChanged += new EventHandler(cbProduct_SelectedIndexChanged);
        }
        private void cbProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbProduct.SelectedIndex != -1)
            {
                txtQty.Text = "";
                GetDetails();
            }
        }
        private void GetDetails()
        {
            string qry = "Select * from Product where proID = " + Convert.ToInt32(cbProduct.SelectedValue) + "";
            SqlCommand cmd = new SqlCommand(qry, MainClass.sqlCon);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if(dt.Rows.Count > 0)
            {
                txtCost.Text = dt.Rows[0]["pCost"].ToString();
                Calculate();
            }
        }

        private void Calculate()
        {
            double qty = 0;
            double cost = 0;
            double amt = 0;
            double.TryParse(txtQty.Text, out qty);
            double.TryParse(txtCost.Text, out cost);
            amt = qty * cost;
            txtAmount.Text = amt.ToString();

        }

        private void txtQty_TextChanged(object sender, EventArgs e)
        {
            Calculate();
        }

        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                string qry = "Select * from Product where pBarcode like '" + txtBarcode.Text + "'";
                SqlCommand cmd = new SqlCommand(qry, MainClass.sqlCon);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    cbProduct.SelectedValue = Convert.ToInt32(dt.Rows[0]["proID"].ToString());
                    Calculate();
                    txtBarcode.Text = "";
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
               if (MainClass.validation(this) == false)
               {
                 //Console.WriteLine("hhh");
                  guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                  guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                  guna2MessageDialog1.Show("please remove error");
                  return;
                }
            
                string pid;
                string pname;
                string qty;
                string cost;
                string amt;
                pname = cbProduct.Text;
                pid = cbProduct.SelectedValue.ToString();
                qty = txtQty.Text;
                cost = txtCost.Text;
                amt = txtAmount.Text;
                guna2DataGridView1.Rows.Add(0, 0, pid, pname, qty, cost, amt);

                cbProduct.SelectedIndex = -1;
                txtQty.Text = "";
                txtCost.Text = "";
                txtAmount.Text = "";
            
            

        }

        private void guna2DataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int count = 0;
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                count++;
                row.Cells[0].Value = count;
            }
        }

        public override void btmSave_Click(object sender, EventArgs e)
        {
            if (guna2DataGridView1.Rows.Count == 0)
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Error;
                guna2MessageDialog1.Show("please fill the form correctly");
                return;
            }
            string qry1 = "";
            string qry2 = "";
            int record = 0;
            if (mainID == 0)
            {
                qry1 = @"Insert into tblMain Values(@date,@type, @supID);
                          Select SCOPE_IDENTITY()";
            }
            else
            {
                qry1 = @"UPDATE tblMain set mdate= @date, mType=@type, mSupCusID=@supID
                      where MainID= @id";

            }
            SqlCommand cmd1 = new SqlCommand(qry1,MainClass.sqlCon);
            cmd1.Parameters.AddWithValue("@id", mainID);
            cmd1.Parameters.AddWithValue("@date", Convert.ToDateTime(txtDate.Value).Date);
            cmd1.Parameters.AddWithValue("@type", "PUR");
            cmd1.Parameters.AddWithValue("@supID", Convert.ToInt32(cbSupplier.SelectedValue));

            if (MainClass.sqlCon.State == ConnectionState.Closed) { MainClass.sqlCon.Open(); }
            if(mainID== 0)
            {
                mainID = Convert.ToInt32(cmd1.ExecuteScalar());
            }
            else
            {
                cmd1.ExecuteNonQuery();

            }
            foreach (DataGridViewRow row in guna2DataGridView1.Rows)
            {
                int did = Convert.ToInt32(row.Cells["dgvid"].Value);
                if(did == 0)
                {
                    qry2 = @"Insert into tblDetails Values(@mID,@proID,@qty,@price,@amount,@cost)";

                }
                else
                {
                    qry2 = @"Update tblDetails set dMainID = @mID,productID = @proID,
                          qty = @qty, price = @price, amount = @amount, cost = @cost
                          where detailID = @id";
                }
                try {
                    SqlCommand cmd2 = new SqlCommand(qry2, MainClass.sqlCon);
                    cmd2.Parameters.AddWithValue("@id", did);
                    cmd2.Parameters.AddWithValue("@mID", mainID);
                    cmd2.Parameters.AddWithValue("@proID", Convert.ToInt32(row.Cells["dgvproid"].Value));
                    cmd2.Parameters.AddWithValue("@qty", Convert.ToInt32(row.Cells["dgvqty"].Value));
                    cmd2.Parameters.AddWithValue("@price", Convert.ToDouble(row.Cells["dgvCost"].Value));
                    cmd2.Parameters.AddWithValue("@amount", Convert.ToDouble(row.Cells["dgvAmount"].Value));
                    cmd2.Parameters.AddWithValue("@cost", Convert.ToDouble(row.Cells["dgvCost"].Value));
                    record += cmd2.ExecuteNonQuery();
               }
                
                catch(Exception ex)
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
                mainID = 0;
                supID = 0;
                txtDate.Value = DateTime.Now;
                cbSupplier.SelectedIndex = -1;
                guna2DataGridView1.Rows.Clear();

            }


        }
        private void loadForEdit()
        {
            string qry = "Select * from tblDetails inner join Product on proID = productID  where dMainID= " + mainID + " ";
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
                pid = row["productID"].ToString();
                qty = row["qty"].ToString();
                cost =row["price"].ToString();
                amt = row["amount"].ToString();
                guna2DataGridView1.Rows.Add(0, did, pid, pname, qty, cost, amt);
            }
        }
    }
}
