using epark.Reports;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace epark.View
{
    public partial class frmReports : Sample
    {
        public frmReports()
        {
            InitializeComponent();
        }

        private DataTable dTable(string qry)
        {
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(qry, MainClass.sqlCon);
            SqlDataAdapter da =new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }
        private void btnProductList_Click(object sender, EventArgs e)
        {
            string qry = @"Select * from Product p inner join Category c on p.pCatID =c.catID";

            DataTable dt = dTable(qry);
            frmPrint frm= new frmPrint();
            CrystalReport5 cr = new CrystalReport5();
            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }

        private void btnStock_Click(object sender, EventArgs e)
        {
            string qry = @"Select proID, pName,
                        (Select SUM(qty) from tblDetails d inner join tblMain m on m.MainID=d.dMainID where m.mType ='PUR' and d.productID=proID) 
                      - (Select SUM(qty) from tblDetails d inner join tblMain m on m.MainID=d.dMainID where m.mType = 'SAL' and d.productID= proID) 
                        as stock from Product";
            DataTable dt = dTable(qry);
            frmPrint frm = new frmPrint();
            rptStock cr = new rptStock();
            cr.SetDataSource(dt);
            frm.crystalReportViewer1.ReportSource = cr;
            frm.crystalReportViewer1.Refresh();
            frm.Show();
        }
    } // - (Select SUM(qty) from tblDetails d inner join tblMain m on m.MainID=d.dMainID where m.mType = 'SAL' and d.productID= proID)
}
