using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PortHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        BindingList<port> ports = new BindingList<port>();
        private void BtnScan_Click(object sender, EventArgs e)
        {
            ports = port.PortIsUsed();

            

          dataGridView1.DataSource= ports;
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
         this.Text=   ((port)dataGridView1.SelectedRows[0].DataBoundItem).PortID.ToString();
        }
    }
}
