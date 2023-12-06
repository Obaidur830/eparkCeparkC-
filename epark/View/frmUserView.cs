using epark.Model;
using System;
using System.Windows.Forms;

namespace epark.View
{
    public partial class frmUserView : SampleView
    {
        public frmUserView()
        {
            InitializeComponent();
        }

        private void frmUserView_Load(object sender, EventArgs e)
        {
            loadData();
        }

        public override void btnAdd_Click(object sender, EventArgs e)
        {
            MainClass.BlurBackground(new formUserAdd());
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
            lb.Items.Add(dgvPass);
            lb.Items.Add(dgvphone);

            string qry = @"Select userID, uUserName, uPass, uPhone from users
                         where uUserName like '%" + txtBoxSearch.Text + "%' order by userID ";
            MainClass.Loaddata(qry, guna2DataGridView1,lb);
        }
    }
}
 