using epark.Model;
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

namespace epark.View
{
    public partial class frmProductView : SampleView
    {
        public frmProductView()
        {
            InitializeComponent();
        }

        private void frmProductView_Load(object sender, EventArgs e)
        {
            loadData();
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new frmProductAdd());
            loadData();
        }


        public override void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {
            loadData();
        }
        private void loadData()
        {
            ListBox lb = new ListBox();
            lb.Items.Add(dgvid);
            lb.Items.Add(dgvname);
            lb.Items.Add(dgvcatID);
            lb.Items.Add(dgvCategory);
            lb.Items.Add(dgvbarcode);
            lb.Items.Add(dgvCost);
            lb.Items.Add(dgvSale);

            string qry = @"Select proID, pName, pCatID, catName, pBarcode, pCost, pPrice from Product
                         inner join Category on catID = pCatID
                         where pName like '%" + txtBoxSearch.Text + "%' order by proID desc ";
            MainClass.Loaddata(qry, guna2DataGridView1, lb);
        }

        

        private void guna2DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //update
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                frmProductAdd frm = new frmProductAdd();
                frm.id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.txtPName.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvname"].Value);
                frm.catID= Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvcatID"].Value);
                frm.txtBarcode.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvbarcode"].Value);
                frm.txtCost.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvCost"].Value);
                frm.txtSalePrice.Text = Convert.ToString(guna2DataGridView1.CurrentRow.Cells["dgvSale"].Value);

                MainClass.BlurBackground(frm);
                loadData();
            }
            //delete
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvDel")
            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;

                if (guna2MessageDialog1.Show("are you really want to delete?") == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                    string qry = "Delete from Product where proID = " + id + "";

                    Hashtable ht = new Hashtable();
                    if (MainClass.SQ1(qry, ht) > 0)
                    {
                        guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK;
                        guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                        guna2MessageDialog1.Show("Deleted Successfully");
                        loadData();
                    }
                }

            }
        }
    }
}
