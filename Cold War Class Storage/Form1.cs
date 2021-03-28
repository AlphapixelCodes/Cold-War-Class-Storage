using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Cold_War_Class_Storage
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
          //  Data.LoadDataFromFile(@"C:\Users\Nick\source\repos\Cold War Class Storage\Cold War Class Storage\Data.ColdWar");
        }
        public enum SearchSettings { Favorite, Name, Gun, None,Primary,Secondary }
        public SearchSettings SearchStyle = SearchSettings.None;
        private String SearchName = "";
        private String SaveFilePath = "";
        public List<Data.GunBuild> GunBuildsDisplayed;
        private bool GunSearchBarRefreshHandled = true;
        public void RefreshTable()
        {
            if (GunSearchBarRefreshHandled)
            {
                LoadGunComboBox();
                GunSearchBarRefreshHandled = false;
            }
            Console.WriteLine("Refresh: " + SearchStyle.ToString());
            switch (SearchStyle)
            {
                case SearchSettings.Favorite:
                    GunBuildsDisplayed = Data.GunBuildList.FindAll(e => e.Favorite);
                    break;
                case SearchSettings.Gun:
                    if (GunSearchComboBox.Selected && !GunSearchComboBox.SelectedItem.ToString().Equals("None"))
                        GunBuildsDisplayed = Data.GunBuildList.FindAll(e => GunSearchComboBox.SelectedItem.ToString().Equals(e.GunClass.Name));
                    break;
                case SearchSettings.Name:
                    GunBuildsDisplayed = Data.GunBuildList.FindAll(e => e.Name.ToLower().Contains(SearchName));
                    break;
                case SearchSettings.None:
                    GunBuildsDisplayed = Data.GunBuildList;
                    break;
                case SearchSettings.Primary:
                    GunBuildsDisplayed = Data.GunBuildList.FindAll(e => e.GunClass.Primary == true);
                    break;
                case SearchSettings.Secondary:
                    GunBuildsDisplayed = Data.GunBuildList.FindAll(e => e.GunClass.Primary == false);
                    break;
            }
            DisplayGunBuilds(GunBuildsDisplayed);
        }
        public void DisplayGunBuilds(List<Data.GunBuild> builds)
        {
            GunBuildsDisplayed = builds;
            BuildGridView.Rows.Clear();
            foreach (var item in builds ?? new List<Data.GunBuild>())
            {

                BuildGridView.Rows.Add(imageList1.Images[item.Favorite ? 1 : 0], item.Name, item.GunClass.Name, (item.GunClass.Primary ? "Primary" : "Secondary"), item.Optic.Name, item.Muzzle.Name, item.Barrel.Name, item.Body.Name, item.Underbarrel.Name, item.Magazine.Name, item.Handle.Name, item.Stock.Name);
                //GunBuildTable.Rows.Add(imageList1.Images[item.Favorite ? 1 : 0], item.Name, item.GunClass.ID, item.Optic.ID, item.Muzzle.ID, item.Barrel.ID, item.Body.ID, item.Underbarrel.ID, item.Magazine.ID, item.Handle.ID, item.Stock.ID);
            }
        }

       

        private void AddBuild_Click(object sender, EventArgs e)
        {
            new EditBuild(null).ShowDialog();
            RefreshTable();
        }

        //edit selected build
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            if (BuildGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Must Select A Row First");
                return;
            }
            new EditBuild(GunBuildsDisplayed[BuildGridView.SelectedCells[0].RowIndex]).ShowDialog();
            RefreshTable();
        }

        private void RefreshClick(object sender, EventArgs e)
        {
            RefreshTable();
        }

        private void ClearSearchSettingsExcept(SearchSettings s)
        {
            SearchStyle = s;
            if (s != SearchSettings.Favorite)
                FavoritesToolStrip.Checked = false;
            if (s != SearchSettings.Gun) { 
                GunSearchComboBox.SelectedItem = "None";
                GunToolstrip.Checked = false;
            }
            if (s != SearchSettings.Name)
                NameToolStrip.Checked = false;
            if (s != SearchSettings.Primary)
                primaryToolStripMenuItem.Checked = false;
            if (s != SearchSettings.Secondary)
                secondaryToolStripMenuItem.Checked = false;
            if (s != SearchSettings.None)
                noneToolStripMenuItem.Checked = false;
            if(s!= SearchSettings.Primary && s != SearchSettings.Secondary)
                typeToolStripMenuItem.Checked = false;
            RefreshTable();
        }

        private void Favorites_Click(object sender, EventArgs e)
        {
            FavoritesToolStrip.Checked = true;
            ClearSearchSettingsExcept(SearchSettings.Favorite);
            /*  GunToolstrip.Checked = false;
              noneToolStripMenuItem.Checked = false;
              NameToolStrip.Checked = false;
              primaryToolStripMenuItem.Checked = false;
              secondaryToolStripMenuItem.Checked = false;
              GunSearchComboBox.SelectedItem = "None";
              RefreshTable();
            */
        }

        private void noneToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClearSearchSettingsExcept(SearchSettings.None);
            noneToolStripMenuItem.Checked = true;
            /*FavoritesToolStrip.Checked = false;
            GunToolstrip.Checked = false;
            
            primaryToolStripMenuItem.Checked = false;
            secondaryToolStripMenuItem.Checked = false;
            NameToolStrip.Checked = false;
            GunSearchComboBox.SelectedItem = "None";
            RefreshTable();*/
        }
        private void LoadGunComboBox()
        {
            
                GunSearchComboBox.Items.Clear();
                Data.GunList.ForEach(e =>
                {
                    GunSearchComboBox.Items.Add(e.Name);
                });
                GunSearchComboBox.SelectedItem = "None";
        }
        private void Form1_Load(object sender, EventArgs e)
        {           
            noneToolStripMenuItem.Click += noneToolStripMenuItem_Click;
            FavoritesToolStrip.Click += Favorites_Click;
            SearchNameTextBox.KeyDown += SearchNameTextBox_KeyDown;
            GunSearchComboBox.SelectedIndexChanged += GunSearchComboBox_SelectedIndexChanged;
            LoadLastSave();
            Closing += new CancelEventHandler(ParentClosing);
        }

      

        private void ParentClosing(object sender, CancelEventArgs e)
        {
            if (Data.AttachmentList.Count == 0 || !Data.HasDataBeenChangedSinceSave)
                return;
            if(MessageBox.Show("Do you want to save before exiting?", "Save?", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                saveAsToolStripMenuItem_Click("",new EventArgs());
            }
        }

        private void GunSearchComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!GunSearchComboBox.SelectedItem.ToString().Equals("None") && GunSearchComboBox.Items.Count>0)
            {
                ClearSearchSettingsExcept(SearchSettings.Gun);
                GunToolstrip.Checked = true;
            }
        }

        private void SearchNameTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                NameToolStrip.Checked = true;
                SearchName = SearchNameTextBox.Text.ToLower();
                ClearSearchSettingsExcept(SearchSettings.Name);
            }
        }
        //primary secondary search
        private void primarysecondarysearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Text == "Primary")
            {
                primaryToolStripMenuItem.Checked = true;
                ClearSearchSettingsExcept(SearchSettings.Primary);
                typeToolStripMenuItem.Checked = true;
            }
            else
            {
                ClearSearchSettingsExcept(SearchSettings.Secondary);
                secondaryToolStripMenuItem.Checked = true;
                typeToolStripMenuItem.Checked = true;
            }
        }


        ///Save Method Group Start
        //open file
        private void openFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog odf = new OpenFileDialog())
            {
                odf.CheckFileExists = true;
                odf.Filter = "Cold War Files (*.coldwar)|*coldwar|All files (*.*)|*.*";
                if (odf.ShowDialog() == DialogResult.OK)
                {
                    LoadFile(odf.FileName);
                }
            }
        }
        private void LoadFile(string fileName)
        {
            updateLastSave(fileName);
            Data.ClearData();
            Data.LoadDataFromFile(fileName);
            LoadGunComboBox();
            RefreshTable();
            Text = Path.GetFileName(fileName);
            Data.HasDataBeenChangedSinceSave = true;
        }
        //Save
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Data.SaveToFile(SaveFilePath);
            MessageBox.Show("Saved to: " + Path.GetFileName(SaveFilePath));
            this.Text = Path.GetFileName(SaveFilePath);


        }
        //save as
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Cold War Files (*.coldwar)|*coldwar|All files (*.*)|*.*";
            sfd.DefaultExt = "coldwar";
            sfd.FileName = Path.GetFileName(SaveFilePath);
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                Data.SaveToFile(sfd.FileName);
                SaveFilePath = sfd.FileName;
                MessageBox.Show("Saved to: " + Path.GetFileName(SaveFilePath));
                updateLastSave(sfd.FileName);
                Text = Path.GetFileName(SaveFilePath);
            }
        }
        private bool hasLastSave()
        {
            return File.Exists(Path.GetTempPath() + "coldwarclass.txt");
        }
        private void LoadLastSave()
        {
            if (hasLastSave())
            {
                string p = File.ReadAllText(Path.GetTempPath() + "coldwarclass.txt");
                Data.ClearData();
                Data.LoadDataFromFile(p);
                RefreshTable();
                updateLastSave(p);
                Text = Path.GetFileName(SaveFilePath);
            }
        }
        private void updateLastSave(String filename)
        {
            
            SaveFilePath = filename;
            String temp = Path.GetTempPath() + "coldwarclass.txt";
            Console.WriteLine(temp);
            if (File.Exists(temp))
            {
                File.Delete(temp);
                
            }
            File.WriteAllText(temp, filename);
        }
        ///Save Method Group End


        //delete selected row
        private void DeleteSelected(object sender, EventArgs e)
        {
            if (BuildGridView.SelectedCells.Count == 0)
            {
                MessageBox.Show("Must Select A Row First");
                return;
            }
            Data.GunBuild b = GunBuildsDisplayed[BuildGridView.SelectedCells[0].RowIndex];
            if (MessageBox.Show( "Are you sure you want to delete \"" + b.Name + "\"?", "Delete class", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                Data.RemoveGunBuild(b);
            RefreshTable();
        }

        //addgunattachment gui
        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            new AddGunAttachment().ShowDialog();
            LoadGunComboBox();
        }
        
        private DataViewer dv;
        //Open Data viewer with raw data
        private void toolStripButton4_Click(object sender, EventArgs e)
        {
            if (dv != null)
                dv.Close();
            new DataViewer(Data.GenerateText()).Show();
                    
        }

        //double click column
        private void BuildGridView_DoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            new BuildViewer(GunBuildsDisplayed[BuildGridView.SelectedCells[0].RowIndex]).Show();
        }

        ///Right Click Start
        private void BuildGridView_Click(object sender, EventArgs e)
        {

            MouseEventArgs a = (MouseEventArgs)e;
            if (a.Button == MouseButtons.Right)
            {
                Point p = new Point(a.X, a.Y);
                int currentMouseOverRow = BuildGridView.HitTest(p.X, p.Y).RowIndex;
                if (currentMouseOverRow >= 0)
                {
                    buildRightClicked = GunBuildsDisplayed[currentMouseOverRow];
                    ContextMenu contextMenu = new ContextMenu();
                    contextMenu.MenuItems.Add("Edit").Click +=contextMenu_ItemClicked;
                    contextMenu.MenuItems.Add("View").Click += contextMenu_ItemClicked;
                    contextMenu.MenuItems.Add("Delete").Click += contextMenu_ItemClicked;
                    contextMenu.MenuItems.Add("Duplicate").Click += contextMenu_ItemClicked;
                    BuildGridView.ContextMenu = contextMenu;
                    contextMenu.Show(BuildGridView, p);
                }
            }
        }
        private Data.GunBuild buildRightClicked;
        private void contextMenu_ItemClicked(object sender, EventArgs e)
        {
            switch(((MenuItem)sender).Text){
                case "Edit":
                    new EditBuild(buildRightClicked).ShowDialog();
                    RefreshTable();
                    break;
                case "Delete":
                    if (MessageBox.Show("Are you sure you want to delete \"" + buildRightClicked.Name + "\"?", "Delete class", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                        Data.RemoveGunBuild(buildRightClicked);
                    RefreshTable();
                    break;
                case "View":
                    new BuildViewer(buildRightClicked).Show();
                    break;
                case "Duplicate":
                    new EditBuild(buildRightClicked.clone()).ShowDialog();
                    break;
            }
        }
        ///Right Click End

        ///Social Media Start
        private void youtubeChannelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.youtube.com/channel/UCyTsi1Tja0kkDv26gX5CjxQ");
        }
        private void githubToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/AlphapixelCodes");
        }
       

        ///Social Media End
    }
}
