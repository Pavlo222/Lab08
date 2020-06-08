using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab8_1
{
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
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            OpenFileDialog fl = new OpenFileDialog();
            List<UniversityLecturer> data = new List<UniversityLecturer>();
            fl.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (fl.ShowDialog() == DialogResult.OK)
            {
                data = ReadDate(fl.FileName);
            }
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Головне меню:\nA) Додавання записiв\nR) Знищення записiв\nC) Редагування записiв\nD) Сортування за датою\nT) Сортування за робочим часом i за зарплатою\nS) Виведення записiв на екран");
                var k = Console.ReadKey().Key;
                if (k == ConsoleKey.A)
                {
                    Add(data);
                }
                if (k == ConsoleKey.R)
                {
                    Console.Clear();
                    Table(data);
                    Remove(data);
                }
                if (k == ConsoleKey.C)
                {
                    Console.Clear();
                    Table(data);
                    ChangeData(data);
                }
                if (k == ConsoleKey.D)
                {
                    data.Sort(new UniversityLecturer.SortByDate());
                    Console.Clear();
                    Table(data);
                    Console.WriteLine("Натиснiть будь яку кнопку для повернення в головне меню");
                    Console.ReadKey();
                }
                if (k == ConsoleKey.T)
                {
                    data.Sort(new UniversityLecturer.SortByWorkTimeAndPayment());
                    Console.Clear();
                    Table(data);
                    Console.WriteLine("Натиснiть будь яку кнопку для повернення в головне меню");
                    Console.ReadKey();
                }
                if (k == ConsoleKey.S)
                {
                    Console.Clear();
                    Table(data);
                    Console.WriteLine("Натиснiть будь яку кнопку для повернення в головне меню");
                    Console.ReadKey();
                }
                SaveDate(data, fl.FileName);
            }
        }
        static void Table(List<UniversityLecturer> v)
        {
            string[] Texts = new string[5];
            Texts[0] = "     Iм'я  ";
            Texts[1] = " Прiзвище ";
            Texts[2] = " Дата працевлаштування ";
            Texts[3] = "    Стаж    ";
            Texts[4] = " Зарплата ";
            Console.WriteLine($"|{Texts[0]}|{Texts[1]}|{Texts[2]}|{Texts[3]}|{Texts[4]}|");
            foreach (UniversityLecturer vg in v)
            {
                Console.WriteLine("|" + vg.Name + s(Texts[0].Length - vg.Name.Length) + "|" +
                    vg.LastName + s(Texts[1].Length - vg.LastName.Length) + "|" +
                    vg.EmploymentDate.Date.ToString("dd.MM.yyyy") + s(Texts[2].Length - vg.EmploymentDate.Date.ToString("dd.MM.yyyy").Length) + "|" +
                    vg.WorkTime + s(Texts[3].Length - vg.WorkTime.ToString().Length) + "|" +
                    vg.Payment + s(Texts[4].Length - vg.Payment.ToString().Length) + "|"
                    );
            }
            
        }
        static void Add(List<UniversityLecturer> v)
        {
            Console.Clear();
            Console.WriteLine("Режим додавання записiв: ");
            UniversityLecturer New = new UniversityLecturer();
            Console.WriteLine("Ввести Iм'я: ");
            New.Name = Console.ReadLine();
            Console.WriteLine("Ввести прiзвище: ");
            New.LastName = Console.ReadLine();
            Console.WriteLine("Ввести дату прийняття на роботу: ");
            New.EmploymentDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
            Console.WriteLine("Ввести стаж: ");
            try
            {
                New.WorkTime = (float)Convert.ToDouble(Console.ReadLine());
            }
            catch
            {

            }
            Console.WriteLine("Ввести зарплату: ");
            New.Payment = Convert.ToInt32(Console.ReadLine());
            v.Add(New);
        }
        static void Remove(List<UniversityLecturer> v)
        {
            Console.WriteLine("Ввести прiзвище запис якого потрiбно знищити: ");
            string name = Console.ReadLine();
            v.RemoveAt(v.FindIndex(f => f.LastName == name));
        }
        static void ChangeData(List<UniversityLecturer> v)
        {
            Console.WriteLine("Ввести назву запис якого потрiбно редагувати: ");
            string name = Console.ReadLine();
            if ((v.FindIndex(f => f.Name == name) != -1))
            {
                UniversityLecturer Change = v[v.FindIndex(f => f.Name == name)];
                Console.WriteLine("1)Редагувати Iм'я\n2)Редагувати прiзвище\n3)Редагувати дату влаштування на роботу\n4)Редагувати стаж\n5)Редагувати зарплату");
                var res = Console.ReadKey().KeyChar;
                Console.WriteLine("\nВвести значення для замiни");
                if (res == '1')
                {
                    Change.Name = Console.ReadLine();
                }
                if (res == '2')
                {
                    Change.LastName = Console.ReadLine();
                }
                if (res == '3')
                {
                    Change.EmploymentDate = DateTime.ParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture);
                }
                if (res == '4')
                {
                    Change.WorkTime = Convert.ToInt16(Console.ReadLine());
                }
                if (res == '5')
                {
                    Change.Payment = Convert.ToInt16(Console.ReadLine());
                }
            }
            else
            {
                Console.WriteLine("Введеного м'я немає в списку");
                Console.ReadKey();
            }

        }
        public static string s(int c)
        {
            try
            {
                return new String(' ', c);
            }
            catch
            {
                return "";
            }
        }
        public static void SaveDate(List<UniversityLecturer> Date, string path)
        {
            using (StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.Default))
            {
                foreach (UniversityLecturer g in Date)
                {

                    sw.WriteLine( g.Name.Trim() + "|" + g.LastName + "|" + g.EmploymentDate.Date.ToString("dd.MM.yyyy") + "|" + g.WorkTime + "|" + g.Payment + "/");

                }
            }
        }
        public static List<UniversityLecturer> ReadDate(string path)
        {
            List<UniversityLecturer> g = new List<UniversityLecturer>();
            string text = "";
            using (StreamReader sr = new StreamReader(path,Encoding.Default))
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
    }
}
