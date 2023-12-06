using System;

namespace epark
{
    public partial class SampleAdd: Sample
    {
        public SampleAdd()
        {
            InitializeComponent();
            guna2Panel1.Show();
        }

        public virtual void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public virtual void btmSave_Click(object sender, EventArgs e)
        {

        }
    }
}
