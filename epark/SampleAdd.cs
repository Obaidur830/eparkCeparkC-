using System;
using System.Windows.Forms;

namespace epark
{
    public partial class SampleAdd : Sample
    {
        public SampleAdd()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = mainForm.Instance;
            //guna2Panel1.Show();
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
