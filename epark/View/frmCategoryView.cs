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
    public partial class frmCategoryView : SampleView
    {
        public frmCategoryView()
        {
            InitializeComponent();
        }

       private void frmCategoryView_Load(object sender, EventArgs e)
        {
           loadData();
        } 
        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new frmCategoryAdd());
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
           

            string qry = @"Select * from Category
                         where catName like '%" + txtBoxSearch.Text + "%' order by catID";
            MainClass.Loaddata(qry, guna2DataGridView, lb);
        }

    

        private void guna2DataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (guna2DataGridView.CurrentCell.OwningColumn.Name == "dgvEdit")
            {
                frmCategoryAdd frm = new frmCategoryAdd();
                frm.id = Convert.ToInt32(guna2DataGridView.CurrentRow.Cells["dgvid"].Value);
                frm.txtCatName.Text = Convert.ToString(guna2DataGridView.CurrentRow.Cells["dgvname"].Value);

                MainClass.BlurBackground(frm);
                loadData();
            }
            //delete
            if (guna2DataGridView.CurrentCell.OwningColumn.Name == "dgvDel")

            {
                guna2MessageDialog1.Buttons = Guna.UI2.WinForms.MessageDialogButtons.YesNo;
                guna2MessageDialog1.Icon = Guna.UI2.WinForms.MessageDialogIcon.Information;
                if (guna2MessageDialog1.Show("are you really want to delete?") == DialogResult.Yes)
                {
                    int id = Convert.ToInt32(guna2DataGridView.CurrentRow.Cells["dgvid"].Value);
                    string qry = "Delete from Category where catID = " + id + "";

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
