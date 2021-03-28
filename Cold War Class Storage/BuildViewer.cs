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
    public partial class BuildViewer : Form
    {
        public BuildViewer(Data.GunBuild build)
        {
            InitializeComponent();
            Build = build;
        }

        public Data.GunBuild Build { get; }

        private void BuildViewer_Load(object sender, EventArgs e)
        {
            this.Text = Build.Name;
            BuildNamelabel.Text = Build.Name;
            GunLabel.Text = Build.GunClass.Name;
            Data.Attachment[] atts = new Data.Attachment[] { Build.Optic, Build.Muzzle, Build.Barrel, Build.Body, Build.Underbarrel, Build.Magazine, Build.Handle, Build.Stock };
            List<Data.Attachment> notnone = atts.ToList().FindAll(a => a.Name != "None");
            Label[] labels = new Label[] {a1,a2,a3,a4,a5,a6,a7,a8};
            for (int i = 0; i < notnone.Count; i++)
            {
                labels[i].Text = notnone[i].Name;
            }
        }
    }
}
