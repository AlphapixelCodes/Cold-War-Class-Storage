using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cold_War_Class_Storage
{
    public partial class DataViewer : Form
    {
        public DataViewer(String text)
        {
            InitializeComponent();
            richTextBox1.Text = text;
        }

        private void DataViewer_Load(object sender, EventArgs e)
        {

        }
    }
}
