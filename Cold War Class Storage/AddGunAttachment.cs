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
    public partial class AddGunAttachment : Form
    {
        public AddGunAttachment()
        {
            InitializeComponent();
        }
        private int getNextAvailableAttachmentID()
        {
            int max = 0;
            Data.AttachmentList.ForEach(e =>
            {
                if (e.ID >= max)
                    max = e.ID + 1;
            });
            return max;
        }
        private int getNextAvailableGunID()
        {
            int max = 0;
            Data.GunList.ForEach(e =>
            {
                if (e.ID >= max)
                    max = e.ID + 1;
            });
            return max;
        }

        private void addGun_Click(object sender, EventArgs e)
        {
            if (validateLength(GunNameTextbox.Text))
            {
                int id = getNextAvailableGunID();
                Data.GunList.Add(new Data.Gun(id, GunNameTextbox.Text, PrimaryCombo.SelectedItem.ToString().Equals( "Primary")));
                MessageBox.Show("Successfully added " + GunNameTextbox.Text + " at ID: " + id);
                Data.HasDataBeenChangedSinceSave = true;
            }
        }

        private bool validateLength(string text)
        {
            if (text.Length <= 3)
            {
                MessageBox.Show("Name must be longer than 3 characters");
                return false;
            }
            return true;

        }

        private void addAttachment(object sender, EventArgs e)
        {
            if (AttachmentTypeCombo.SelectedItem != null)
            {
                if (validateLength(AttachmentTextbox.Text))
                {
                    int id = getNextAvailableAttachmentID();
                    Console.WriteLine("AddGunAttachment.addAttachment: " + (AttachmentTypeCombo.SelectedIndex));
                    Data.AttachmentList.Add(new Data.Attachment(id, AttachmentTextbox.Text, Data.GetAttachmentTypeByID(AttachmentTypeCombo.SelectedIndex+1).ID));
                    MessageBox.Show("Successfully added " + AttachmentTextbox.Text + " at ID: " + id);
                    Data.HasDataBeenChangedSinceSave = true;
                }
            }
            else
            {
                MessageBox.Show("Must select attachment type");
            }
        }

        private void AddGunAttachment_Load(object sender, EventArgs e)
        {
            PrimaryCombo.SelectedItem = "Primary";
        }
    }
}
