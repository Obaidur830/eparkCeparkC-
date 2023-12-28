using System;
using System.Windows.Forms;


namespace epark
{
    public partial class SampleView : Sample
    {
        public SampleView()
        {
            InitializeComponent();
            guna2MessageDialog1.Parent = mainForm.Instance;
        }



        public virtual void btnAdd_Click(object sender, EventArgs e)
        {

        }


        public virtual void txtBoxSearch_TextChanged(object sender, EventArgs e)
        {

        }

        public virtual void SampleView_Load(object sender, EventArgs e)
        {

        }

    }
}
