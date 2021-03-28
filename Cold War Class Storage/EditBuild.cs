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
    public partial class EditBuild : Form
    {
        Data.GunBuild build;
      //  private List<Data.Attachment> OpticList, MuzzleList, BarrelList, BodyList, UnderbarrelList, MagazineList, HandleList, StockList;
        //private List<Data.Gun> GunList;
        //clear all button
        private void button3_Click(object sender, EventArgs e)
        {
            ClearAllInputs();
        }

        public EditBuild(Data.GunBuild b)
        {
            build = b;
            InitializeComponent();
            loadDropdowns();
            ClearAllInputs();
            loadBuild();
        }
        private void ClearAllInputs()
        {
            OpticCombo.SelectedItem = "None";
            MuzzleCombo.SelectedItem = "None";
            BarrelCombo.SelectedItem = "None";
            BodyCombo.SelectedItem = "None";
            UnderbarrelCombo.SelectedItem = "None";
            MagazineCombo.SelectedItem = "None";
            HandleCombo.SelectedItem = "None";
            StockCombo.SelectedItem = "None";
            GunCombo.SelectedItem = "None";
            
            FavoriteCheckBox.Checked = false;
            NameBox.Text = "";
        }
        public Data.Attachment getAttachment(List<Data.Attachment> ls, ComboBox bx)
        {
            return ls[bx.SelectedIndex];
        }
        private void updateBuild()
        {
            bool toadd = false;
            if (build == null)
            {
                toadd = true;
                build = new Data.GunBuild("",null,null,null,null,null,null,null,null,null,false);       
            }
            build.Optic = Data.GetAttachmentByName(OpticCombo.SelectedItem.ToString());
            build.Muzzle = Data.GetAttachmentByName(MuzzleCombo.SelectedItem.ToString());
            //Console.WriteLine(Data.GetAttachmentByName(BarrelCombo.SelectedItem.ToString()));
            build.Barrel = Data.GetAttachmentByName(BarrelCombo.SelectedItem.ToString());
            build.Body = Data.GetAttachmentByName(BodyCombo.SelectedItem.ToString());
            build.Underbarrel = Data.GetAttachmentByName(UnderbarrelCombo.SelectedItem.ToString());
            build.Magazine = Data.GetAttachmentByName(MagazineCombo.SelectedItem.ToString());
            build.Handle = Data.GetAttachmentByName(HandleCombo.SelectedItem.ToString());
            build.Stock = Data.GetAttachmentByName(StockCombo.SelectedItem.ToString());
            build.GunClass = Data.GetGunByName(GunCombo.SelectedItem.ToString());
            build.Favorite = FavoriteCheckBox.Checked;
            build.Name = NameBox.Text;
            //if (toadd)
                Data.AddGunBuild(build);
        }
        public bool isValidInput()
        {
            if (NameBox.Text.Length <= 3)
            {
                MessageBox.Show("Name is too short, Must be longer than 3 characters");
                return false;
            }else if (false && Data.GunBuild.isNameValid(NameBox.Text))
            {
                Console.WriteLine("EditBuild.isValidInput: removed is name valid because it sucked");
                //MessageBox.Show("Name Contains Illegal Characters, Must be A-Z or 0-9");
                return false;
            }else if(build==null) {
                if (Data.HasGunBuildWithName(NameBox.Text))
                {
                    MessageBox.Show("A build already exists with that name");
                    return false;
                }
            }else if (NameBox.Text.Contains(","))
            {
                MessageBox.Show("Name cannot contain \",\"");
                return false;
            }
            return true;
        }
        private void loadBuild()
        {
            if (build == null) {
                ClearAllInputs();
                return;
            }
            GunCombo.SelectedItem = build.GunClass.Name;
            OpticCombo.SelectedItem = build.Optic.Name;
            MuzzleCombo.SelectedItem = build.Muzzle.Name;
            BarrelCombo.SelectedItem = build.Barrel.Name;
            BodyCombo.SelectedItem = build.Body.Name;
            UnderbarrelCombo.SelectedItem = build.Underbarrel.Name;
            MagazineCombo.SelectedItem = build.Magazine.Name;
            HandleCombo.SelectedItem = build.Handle.Name;
            StockCombo.SelectedItem = build.Stock.Name;
            FavoriteCheckBox.Checked = build.Favorite;
            NameBox.Text = build.Name;
        }
        private void addDropDownItems(ComboBox c, List<Data.Attachment> attlist)
        {
            c.Items.Clear();

            foreach (var item in attlist)
            {
                c.Items.Add(item.Name);
            }
        }
        private void loadDropdowns()
        {
            addDropDownItems(OpticCombo, Data.GetAttachmentByTypeID(1));
            addDropDownItems(MuzzleCombo, Data.GetAttachmentByTypeID(2));
            addDropDownItems(BarrelCombo, Data.GetAttachmentByTypeID(3));
            addDropDownItems(BodyCombo, Data.GetAttachmentByTypeID(4));
            addDropDownItems(UnderbarrelCombo, Data.GetAttachmentByTypeID(5));
            addDropDownItems(MagazineCombo, Data.GetAttachmentByTypeID(6));
            addDropDownItems(HandleCombo, Data.GetAttachmentByTypeID(7));
            addDropDownItems(StockCombo, Data.GetAttachmentByTypeID(8));
            Data.GunList.ForEach(e => GunCombo.Items.Add(e.Name));

        }
        //close button
        private void button2_Click(object sender, EventArgs e)
        {
            //if(MessageBox.Show("Are you sure you want to exit without saving the build?","Confirm",MessageBoxButtons.YesNo)==DialogResult.Yes)
                this.Close();
        }

        //update
        private void button1_Click(object sender, EventArgs e)
        {
            if (isValidInput())
            {
                updateBuild();
                this.Close();
            }
        }
    }
}
