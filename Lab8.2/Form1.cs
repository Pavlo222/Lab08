using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace Lab08_2
{
    
    public partial class Form1 : Form
    {
        bool adds = false;
        string lb2 = "Детальна інформація\nМожна редагувати";
        string bt5 = "Зберегти зміни об'єкта";
        string bt6 = "Зберегти все у файл";
        OpenFileDialog fl = new OpenFileDialog();
        public class UniversityLecturer
        {
            public string Name = "";
            public string LastName = "";
            public DateTime EmploymentDate;
            public float WorkTime = 0;
            public int Payment = 0;
            public UniversityLecturer(string Name = "", string LastName = "", string EmploymentDate = "00.00.0000", float WorkTime = 0, int Payment = 0)
            {
                this.Name = Name;
                this.LastName = LastName;
                this.WorkTime = WorkTime;
                this.Payment = Payment;
            }
            public int CompareTo(UniversityLecturer p)
            {
                return this.EmploymentDate.CompareTo(p.Payment);
            }
            public class SortByDate : IComparer<UniversityLecturer>
            {
                public int Compare(UniversityLecturer p1, UniversityLecturer p2)
                {
                    if (p1.EmploymentDate > p2.EmploymentDate)
                        return 1;
                    else if (p1.EmploymentDate < p2.EmploymentDate)
                        return -1;
                    else
                        return 0;
                }
            }
            public class SortByPayment : IComparer<UniversityLecturer>
            {
                public int Compare(UniversityLecturer p1, UniversityLecturer p2)
                {
                    if (p1.Payment < p2.Payment)
                        return 1;
                    else if (p1.Payment > p2.Payment)
                        return -1;
                    else
                        return 0;
                }
            }
            public class SortByWorkTime : IComparer<UniversityLecturer>
            {
                public int Compare(UniversityLecturer p1, UniversityLecturer p2)
                {
                    if (p1.WorkTime > p2.WorkTime)
                        return 1;
                    else if (p1.WorkTime < p2.WorkTime)
                        return -1;
                    else
                        return 0;
                }
            }
            public class SortByName : IComparer<UniversityLecturer>
            {
                public int Compare(UniversityLecturer p1, UniversityLecturer p2)
                {
                    if (p1.Name[0] > p2.Name[0])
                        return 1;
                    else if (p1.Name[0] < p2.Name[0])
                        return -1;
                    else
                        return 0;
                }
            }
            public class SortByLastName : IComparer<UniversityLecturer>
            {
                public int Compare(UniversityLecturer p1, UniversityLecturer p2)
                {
                    if (p1.LastName[0] > p2.LastName[0])
                        return 1;
                    else if (p1.LastName[0] < p2.LastName[0])
                        return -1;
                    else
                        return 0;
                }
            }
            public class SortByWorkTimeAndPayment : IComparer<UniversityLecturer>
            {
                public int Compare(UniversityLecturer p1, UniversityLecturer p2)
                {
                    if (p1.WorkTime > p2.WorkTime)
                    {
                        return 1;
                    }
                    else if (p1.WorkTime < p2.WorkTime)
                    {
                        return -1;
                    }
                    else if (p1.Payment > p2.Payment)
                    {
                        return 1;
                    }
                    else if (p1.Payment < p2.Payment)
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            }
        }
        static List<UniversityLecturer> Base; 
        public Form1()
        {
            InitializeComponent();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            while (true)
            {
                if (fl.ShowDialog() == DialogResult.OK)
                {
                    if (Path.GetExtension(fl.FileName) == ".txt")
                    {
                        Base = ReadDate(fl.FileName);
                        button1.Enabled = true;
                        button2.Enabled = true;
                        button5.Enabled = true;
                        button6.Enabled = true;
                        textBox1.Enabled = true;
                        textBox2.Enabled = true;
                        textBox3.Enabled = true;
                        textBox4.Enabled = true;
                        textBox5.Enabled = true;
                        listBox1.Enabled = true;
                        domainUpDown1.Enabled = true;
                        domainUpDown1.SelectedItem = 0;
                        Base.Sort(new UniversityLecturer.SortByName());
                        listBox1.DataSource = GetNames(Base);
                        break;
                    }


                }
                else
                {
                    break;
                }
            }
           
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Base = new List<UniversityLecturer>();
            button1.Enabled = false;
            button2.Enabled = false;
            button5.Enabled = false;
            button6.Enabled = false;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            textBox4.Enabled = false;
            textBox5.Enabled = false;
            listBox1.Enabled = false;
            domainUpDown1.Enabled = false;
        }
        public static void SaveDate(List<UniversityLecturer> Date, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (UniversityLecturer g in Date)
                {

                    sw.WriteLine(g.Name.Trim() + "|" + g.LastName + "|" + g.EmploymentDate.Date.ToString("dd.MM.yyyy") + "|" + g.WorkTime + "|" + g.Payment + "/");

                }
            }
        }
        public static List<UniversityLecturer> ReadDate(string path)
        {
            List<UniversityLecturer> g = new List<UniversityLecturer>();
            string text = "";
            using (StreamReader sr = new StreamReader(path, Encoding.Default))
            {
                text = sr.ReadToEnd();
            }
            string[] Dates = text.Split('/');
            foreach (string s in Dates)
            {
                string[] MetaDete = s.Split('|');
                if (MetaDete.Length == 5)
                {
                    UniversityLecturer d = new UniversityLecturer
                    {
                        Name = MetaDete[0].Trim(),
                        LastName = MetaDete[1],
                        EmploymentDate = DateTime.ParseExact(MetaDete[2], "dd.MM.yyyy", CultureInfo.InvariantCulture),
                        WorkTime = (float)Convert.ToDouble(MetaDete[3]),
                        Payment = Convert.ToInt32(MetaDete[4])
                    };
                    g.Add(d);
                }
            }
            return g;
        }
        public static List<string> GetNames(List<UniversityLecturer> dase)
        {
            List<string> ret = new List<string>();
            foreach(UniversityLecturer i in dase)
            {
                ret.Add(i.LastName + " " + i.Name);
            }
            return ret;
        }
        public int FindObject(string Name)
        {
            foreach(UniversityLecturer i in Base)
            {
                if (i.LastName + " " + i.Name == Name)
                {
                    return Base.IndexOf(i);
                }
            }
            return -1;
        }
        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            if(domainUpDown1.SelectedItem.ToString() == "Ім'ям")
            {
                Base.Sort(new UniversityLecturer.SortByName());
            }
            if (domainUpDown1.SelectedItem.ToString() == "Прізвищем")
            {
                Base.Sort(new UniversityLecturer.SortByLastName());
            }
            if (domainUpDown1.SelectedItem.ToString() == "Датою")
            {
                Base.Sort(new UniversityLecturer.SortByDate());
            }
            if (domainUpDown1.SelectedItem.ToString() == "Зарплатою")
            {
                Base.Sort(new UniversityLecturer.SortByPayment());
            }
            if (domainUpDown1.SelectedItem.ToString() == "Стажом")
            {
                Base.Sort(new UniversityLecturer.SortByWorkTime());
            }
            listBox1.DataSource = GetNames(Base);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string n = (string)listBox1.SelectedItem;
            int i = FindObject(n);
            if (i != -1)
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                textBox1.Text = Base[i].Name;
                textBox2.Text = Base[i].LastName;
                textBox3.Text = Base[i].EmploymentDate.ToString("dd.MM.yyyy");
                textBox4.Text = Base[i].Payment.ToString();
                textBox5.Text = Base[i].WorkTime.ToString();
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!adds)
            {
                string n = (string)listBox1.SelectedItem;
                int i = FindObject(n);
                if (i != -1)
                {
                    Base[i].Name = textBox1.Text;
                    Base[i].LastName = textBox2.Text;
                    try
                    {
                        Base[i].Payment = Convert.ToInt32(textBox4.Text);
                        Base[i].WorkTime = Convert.ToInt32(textBox5.Text);
                        Base[i].EmploymentDate = DateTime.ParseExact(textBox3.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    }
                    catch
                    {
                        label1.Text = "Сталася помилка";
                    }
                }
            }
            else
            {
                UniversityLecturer nw = new UniversityLecturer();
                nw.Name = textBox1.Text;
                nw.LastName = textBox2.Text;
                try
                {
                    nw.EmploymentDate = DateTime.ParseExact(textBox3.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                    nw.Payment = Convert.ToInt32(textBox4.Text);
                    nw.WorkTime = Convert.ToInt32(textBox5.Text);
                }
                catch
                {
                   
                }
                Base.Add(nw);
                domainUpDown1.SelectedItem = 0;
                Base.Sort(new UniversityLecturer.SortByName());
                listBox1.DataSource = GetNames(Base);
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                listBox1.Enabled = true;
                domainUpDown1.Enabled = true;
                button5.Text = bt5;
                button6.Text = bt6;
                label2.Text = lb2;
                adds = false;
            }
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (!adds)
            {
                string n = (string)listBox1.SelectedItem;
                int i = FindObject(n);
                if (i != -1)
                {
                    Base[i].Name = textBox1.Text;
                    Base[i].LastName = textBox2.Text;
                    try
                    {
                        Base[i].EmploymentDate = DateTime.ParseExact(textBox3.Text, "dd.MM.yyyy", CultureInfo.InvariantCulture);
                        Base[i].Payment = Convert.ToInt32(textBox4.Text);
                        Base[i].WorkTime = Convert.ToInt32(textBox5.Text);
                    }
                    catch
                    {
                        
                    }
                }
                SaveDate(Base, fl.FileName);
            }
            else
            {
                
                button1.Enabled = true;
                button2.Enabled = true;
                button3.Enabled = true;
                button4.Enabled = true;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                listBox1.Enabled = true;
                domainUpDown1.Enabled = true;
                button5.Text = bt5;
                button6.Text = bt6;
                label2.Text = lb2;
                adds = false;
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            var fbd = new FolderBrowserDialog();
            while (true)
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    var f = File.Create(fbd.SelectedPath+"/DataBase.txt");
                    fl.FileName = f.Name;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button5.Enabled = true;
                    button6.Enabled = true;
                    textBox1.Enabled = true;
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    textBox4.Enabled = true;
                    textBox5.Enabled = true;
                    listBox1.Enabled = true;
                    domainUpDown1.Enabled = true;
                    domainUpDown1.SelectedItem = 0;
                    Base.Sort(new UniversityLecturer.SortByName());
                    listBox1.DataSource = GetNames(Base);
                    break;
                }
                else
                {
                    break;
                }
            }
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            button5.Text = "Додати";
            button6.Text = "Відмінити";
            label2.Text = "Введіть дані нового користувача";
            listBox1.Enabled = false;
            domainUpDown1.Enabled = false;
            adds = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Base.RemoveAt(Base.FindIndex(f => (f.LastName + " " + f.Name) == (string)listBox1.SelectedItem));
            listBox1.DataSource = GetNames(Base);
        }
    }
}
