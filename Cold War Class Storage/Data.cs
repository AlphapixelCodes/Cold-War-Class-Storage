using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cold_War_Class_Storage
{
    public static class Data
    {
        public static bool HasDataBeenChangedSinceSave = false;
        public static List<Attachment> AttachmentList = new List<Attachment>();
        public static List<Gun> GunList = new List<Gun>();
        public static List<AttachmentType> AttachmentTypeList = new List<AttachmentType>();
        public static List<GunBuild> GunBuildList = new List<GunBuild>();
        public static void AddGunBuild(GunBuild a)
        {
            RemoveGunBuild(a);
            GunBuildList.Add(a);
            Data.HasDataBeenChangedSinceSave = true;
        }
        public static void RemoveGunBuild(GunBuild a)
        {
            if (GunBuildList.Contains(a))
                GunBuildList.Remove(a);
            Data.HasDataBeenChangedSinceSave = true;
        }
        public static Attachment GetAttachmentByID(int id)
        {
            foreach (var a in AttachmentList)
            {
                if (a.ID == id)
                    return a;
            }
            return null;
        }
        public static Attachment GetAttachmentByName(string selectedItem)
        {
            return AttachmentList.Find(e => e.Name == selectedItem);
        }
        public static List<Attachment> GetAttachmentByTypeID(int id)
        {
            return AttachmentList.FindAll(e => e.Type == id);
        }
        public static AttachmentType GetAttachmentTypeByID(int id)
        {
            return AttachmentTypeList.First(e => e.ID == id);
        }

        private static Gun GetGunByID(int id)
        {
            return GunList.Find(e => e.ID == id);
        }
        public static Gun GetGunByName(string name)
        {
            return GunList.Find(e => e.Name == name);
        }
        public static bool HasGunBuildWithName(string name)
        {
            return GunBuildList.Any(e => e.Name == name);
        }
        public static GunBuild GetGunBuildByName(string name)
        {
            return GunBuildList.Find(e => e.Name == name);
        }
        public class GunBuild
        {
            public bool Favorite;
            public Gun GunClass;
            public string Name;
            public Attachment Optic, Barrel, Body, Underbarrel, Magazine, Handle, Stock, Muzzle;

            public GunBuild(string name, Gun gun, Attachment optic, Attachment muzzle, Attachment barrel, Attachment body, Attachment underbarrel, Attachment magazine, Attachment handle, Attachment stock, bool favorite)
            {
                Favorite = favorite;
                Muzzle = muzzle;
                Name = name;
                GunClass = gun;
                Optic = optic;
                Barrel = barrel;
                Body = body;
                Underbarrel = underbarrel;
                Magazine = magazine;
                Handle = handle;
                Stock = stock;
            }
            public override string ToString()
            {
                string op = (Optic != null) ? Optic.ID + "" : "null";
                string mz = (Muzzle != null) ? Muzzle.ID + "" : "null";
                string bar = (Barrel != null) ? Barrel.ID + "" : "null";
                string bod = (Body != null) ? Body.ID + "" : "null";
                string und = (Underbarrel != null) ? Underbarrel.ID + "" : "null";
                string mag = (Magazine != null) ? Magazine.ID + "" : "null";
                string hand = (Handle != null) ? Handle.ID + "" : "null";
                string sk = (Stock != null) ? Stock.ID + "" : "null";
                string fav = Favorite ? "1" : "0";
                if (GunClass == null)
                    return "";
                return String.Format("{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}", Name, GunClass.ID, op, mz, bar, bod, und, mag, hand, sk, fav);
            }
            public static bool isNameValid(string name)
            {
                return Regex.IsMatch(name, "[^a-zA-z0-9]+");
            }
            public bool isDifferent(GunBuild b)
            {
                return Favorite == b.Favorite && GunClass == b.GunClass && Optic == b.Optic && Barrel == b.Barrel && Body == b.Body && Underbarrel == b.Underbarrel && Magazine == b.Magazine && Handle == b.Handle && Stock == b.Stock && Muzzle == b.Muzzle;
            }
            public GunBuild clone()
            {
                return new GunBuild("", GunClass, Optic, Muzzle, Barrel, Body, Underbarrel, Magazine, Handle, Stock, Favorite);
            }
        }
        public class AttachmentType
        {
            public int ID { get; }
            public string Name { get; }
            public AttachmentType(int id, string name)
            {
                ID = id;
                Name = name;
            }
            public override string ToString()
            {
                return ID + "," + Name;
            }
        }
        public class Gun
        {
            public int ID { get; }
            public string Name { get; }
            public bool Primary { get; }
            public Gun(int id, string name, bool primary)
            {
                ID = id;
                Name = name;
                Primary = primary;
            }
            public override string ToString()
            {
                return ID + "," + Name + "," + Primary;
            }
        }
        public class Attachment
        {
            public int ID { get; }
            public int Type { get; }
            public string Name { get; }

            public Attachment(int id, string name, int type)
            {
                ID = id;
                Name = name;
                Type = type;
            }
            public override string ToString()
            {
                return ID + "," + Name + "," + Type;
            }
        }
        public static void ClearData()
        {
            AttachmentList.Clear();
            GunList.Clear();
            AttachmentTypeList.Clear();
            GunBuildList.Clear();
            HasDataBeenChangedSinceSave = true;
        }
        public static bool LoadDataFromFile(string FileName)
        {
            HasDataBeenChangedSinceSave = true;
            if (!File.Exists(FileName))
            {
                Console.WriteLine("Data.LoadDataFromFile: Error: File " + FileName + " Does not exist");
                return false;
            }
            string text = File.ReadAllText(FileName);

            String[] lines = text.Split(new string[] { "//Break//" }, StringSplitOptions.RemoveEmptyEntries);
            string build = "";//build must be last
            bool Noerror = true;
            foreach (var item in lines)
            {
                string type1 = Regex.Replace(item.Split('\n')[0], "[^A-Za-z]", String.Empty);
                switch (type1.Replace("\n", ""))
                {
                    case "AttachmentTypes":

                        Noerror = Noerror && loadAttachmentTypes(item);
                        break;
                    case "Attachment":
                        Noerror = Noerror && loadAttachments(item);
                        break;
                    case "Gun":
                        Noerror = Noerror && loadGuns(item);
                        break;
                    case "Build":
                        build = item;
                        break;
                    default:
                        break;
                }
                if (!Noerror)
                {
                    ClearData();
                    return false;
                }

            }
            if (build.Length > 0)
            {
                Noerror = Noerror && loadBuild(build);
            }
            if (!Noerror)
            {
                ClearData();
                return false;
            }
            Console.WriteLine("load complete Build");
            return true;

        }

        private static bool loadBuild(string txt)
        {
            String[] lines = txt.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    string[] vs = lines[i].Split(new String[] { "," }, StringSplitOptions.None);
                    Attachment[] abc = new Attachment[vs.Length - 3];
                    for (int c = 2; c < vs.Length - 1; c++)
                    {
                        Attachment a = GetAttachmentByID(Int32.Parse(vs[c]));
                        abc[c - 2] = a ?? GetAttachmentByName("None");
                    }
                    bool fav = Regex.Replace(vs[10], "[^01]", String.Empty) == "1";
                    string name = vs[0];
                    while (GunBuildList.Any(e => e.Name.Equals(vs[1])))
                        name += "1";
                    GunBuildList.Add(new GunBuild(name, GetGunByID(Int32.Parse(vs[1])), abc[0], abc[1], abc[2], abc[3], abc[4], abc[5], abc[6], abc[7], fav));
                }
                catch
                {
                    MessageBox.Show("Eror loading data: check Build section plus line: " + i + " in Data file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearData();
                    return false;
                }

            }
            return true;
        }
        private static bool loadAttachments(string txt)
        {
            String[] lines = txt.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    string[] vs = Regex.Replace(lines[i], "[^A-Za-z,0-9/ .-]", String.Empty).Split(new String[] { "," }, StringSplitOptions.None);
                    int id = Int32.Parse(vs[0]);
                    if (vs[1] == "None" || !AttachmentList.Any(e => e.Name == vs[1] || e.ID == id))
                        AttachmentList.Add(new Attachment(id, vs[1], Int32.Parse(vs[2])));
                }
                catch
                {
                    MessageBox.Show("Eror loading data: check Attachments section plus line: " + i + " in Data file: " + lines[i], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearData();
                    return false;
                }
            }
            return true;
        }
        private static bool loadGuns(string txt)
        {
            String[] lines = txt.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    string[] vs = lines[i].Split(new String[] { "," }, StringSplitOptions.None);
                    Console.WriteLine(lines[i]);
                    string name = Regex.Replace(vs[1], "[^A-Za-z0-9& ]", String.Empty);
                    int id = Int32.Parse(vs[0]);
                    bool primary = bool.Parse(vs[2]);
                    if (!GunList.Any(e => e.Name == name || e.ID == id))
                        GunList.Add(new Gun(id, name, primary));
                }
                catch
                {
                    MessageBox.Show("Eror loading data: check Gun section plus line: " + i + " in Data file", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearData();
                    return false;
                }
            }
            return true;
        }
        private static bool loadAttachmentTypes(string txt)
        {
            String[] lines = txt.Split(new String[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < lines.Length; i++)
            {
                try
                {
                    string[] vs = lines[i].Split(new String[] { "," }, StringSplitOptions.None);
                    int id = Int32.Parse(vs[0]);
                    if (vs.Length == 2 && !AttachmentList.Any(e => e.Name == vs[1] || id == e.ID))
                        AttachmentTypeList.Add(new AttachmentType(id, vs[1]));
                }
                catch
                {
                    MessageBox.Show("Eror loading data: check AttachmentTypes section plus line: " + i + " in Data file: " + lines[i], "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ClearData();
                    return false;
                }
            }
            return true;
        }
        public static void SaveToFile(string file)
        {
            HasDataBeenChangedSinceSave = false;
            File.WriteAllText(file, GenerateText());
        }

        public static string GenerateText()
        {
            string r = "AttachmentTypes\n";
            AttachmentTypeList.ForEach(e => { r += e.ToString() + "\n"; });
            r += "//Break//Attachment\n";
            AttachmentList.ForEach(e => { r += e.ToString() + "\n"; });
            r += "//Break//Gun\n";
            GunList.ForEach(e => { r += e.ToString() + "\n"; });
            r += "//Break//Build\n";
            GunBuildList.ForEach(e => { r += e.ToString() + "\n"; });
            r.TrimEnd('\n');
            return r;
        }
    }
}
