using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace epark.Model
{
    public partial class ucProduct : UserControl
    {
        public event EventHandler onSelect = null;

        public ucProduct()
        {
            InitializeComponent();
        }

        private void ucProductbox_Click(object sender, EventArgs e)
        {
            onSelect?.Invoke(this, e);

        }
        public int id { get; set; }
        public string pCost {  get; set; }
        public string pName 
        {
            get { return lbPName.Text; }
            set { lbPName.Text = value; }
        }
        public string Price
        {
            get { return lbPrice.Text; }
            set { lbPrice.Text = value; }
        }
        public Image PImage
        {
            get { return ucProductbox.Image; }
            set { ucProductbox.Image = value; }
        }
    }
}
