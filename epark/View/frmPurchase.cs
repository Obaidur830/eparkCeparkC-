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
    public partial class frmPurchase : SampleView
    {
        public frmPurchase()
        {
            InitializeComponent();
        }

        private void frmPurchase_Load(object sender, EventArgs e)
        {
            loadData();
        }
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new frmPurchaseAdd());
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
            lb.Items.Add(dgvdate);
            lb.Items.Add(dgvSupID);
            lb.Items.Add(dgvSupplier);
            lb.Items.Add(dgvAmount);


            string qry = @"Select dMainID, mdate, m.mSupCusID,s.supName, SUM(d.amount) from tblMain m
                         inner join tblDetails d on d.dMainID = m.MainID
                         inner join Supplier s on s.supID = m.mSupCusID 
                         where m.mType = 'PUR' and supName like '%" + txtBoxSearch.Text + "%'" +
                         "group by dMainID, mdate, m.mSupCusID, s.supName";
            MainClass.Loaddata(qry, guna2DataGridView1, lb);
        }

       

        private void guna2DataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //update
            if (guna2DataGridView1.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                frmPurchaseAdd frm = new frmPurchaseAdd();
                frm.mainID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvid"].Value);
                frm.supID = Convert.ToInt32(guna2DataGridView1.CurrentRow.Cells["dgvSupID"].Value);

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
                    string qry = "Delete from tblMain where mainID = " + id + "";
                    string qry1 = "Delete from tblDetails where dMainID = " + id + "";

                    Hashtable ht = new Hashtable();
                    MainClass.SQ1(qry, ht);
                    if (MainClass.SQ1(qry1, ht) > 0)
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
