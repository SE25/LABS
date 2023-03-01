using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace LAB_6.LVL3
{
    public static class Task1
    {
        public const int numberOfExams = 5;
        struct Student
        {
            string _name;
            int _result;
            double _averageMark;

            public string Name 
            {
                get { return _name; } 
                set { _name = value; }
            }
            public double AverageMark
            {
                get { return _averageMark; }
                set { _averageMark = value; }
            }
            public Student(string name)
            {
                _name = name;
                _result = 0;
                _averageMark = 0;
            }

            public void GetMark()
            {
                int seed = DateTime.Now.Millisecond;
                Random random = new Random(seed);
                int lowestMarkSum = 10;
                int highestMarkSum = 25;
                _result = random.Next(lowestMarkSum, highestMarkSum);
            }

            public void Average()
            {
                _averageMark = _result / numberOfExams;
            }
        }
        public static void LVL3_ex1()
        {
            #region
            string[] namesGroup1 = new string[] { "Gorin", "Orlov", "Rechkin", "Gavrilov", "Razumovskiy" };
            string[] namesGroup2 = new string[] { "Slavin", "Gorkov", "Repkin", "Larchenkov", "Grishin" };
            string[] namesGroup3 = new string[] { "Gon", "Zubkov", "Kormilov", "Terkin", "Teryohin" };
            #endregion
            Student[] students1 = new Student[namesGroup1.Length];
            Student[] students2 = new Student[namesGroup2.Length];
            Student[] students3 = new Student[namesGroup3.Length];

            InitilizeStudents(students1, namesGroup1);
            InitilizeStudents(students2, namesGroup2);
            InitilizeStudents(students3, namesGroup3);

            StudentDelegate paperWork = SetMarks;
            paperWork += CalculateAverage;
            paperWork(students1);
            paperWork(students2);
            paperWork(students3);

            SortByAverage(students1);
            SortByAverage(students2);
            SortByAverage(students3);

            PrintByAverage(students1, students2, students3);
        }

        static void InitilizeStudents(Student[] list, string[] names)
        {
            for(int i = 0; i < list.Length; i++)
            {
                list[i] = new Student(names[i]);
            }
        }
        static void SetMarks(Student[] list)
        {
            for(int i = 0; i < list.Length; i++)
            {
                list[i].GetMark();
            }
        }

        static void CalculateAverage(Student[] list)
        {
            for(int i = 0; i < list.Length; i++)
            {
                list[i].Average();
            }
        }

        static void SortByAverage(Student[] list)
        {
            Student buffer;
            for(int i = 0; i < list.Length; i++)
            {
                for(int j = i + 1; j < list.Length; j++)
                {
                    if (list[i].AverageMark < list[j].AverageMark)
                    {
                        buffer = list[i];
                        list[i] = list[j];
                        list[j] = buffer;
                    }
                }
            }
        }

        static void PrintByAverage(Student[] list1, Student[] list2, Student[] list3)
        {
            double groupAverage1 = CountGroupAverage(list1);
            double groupAverage2 = CountGroupAverage(list2);
            double groupAverage3 = CountGroupAverage(list3);
            (Student[], double)[] list = new (Student[], double)[] { (list1, groupAverage1), (list2, groupAverage2), (list3, groupAverage3) };
            list = list.OrderByDescending(x => x.Item2).ToArray();
            for(int i = 0; i < list.Length; i++)
            {
                Console.WriteLine($"Group: {i + 1} ~ {list[i].Item2}");
                Console.WriteLine("---------------------------------");
                for(int j = 0; j < list[i].Item1.Length; j++)
                {
                    Console.WriteLine($"{list[i].Item1[j].Name} ~ {list[i].Item1[j].AverageMark}");
                }
                Console.WriteLine("---------------------------------");    
            }
        }
        static double CountGroupAverage(Student[] list)
        {
            double groupAverage = 0;
            for(int i = 0; i < list.Length; i++)
            {
                groupAverage += list[i].AverageMark;
            }
            groupAverage = groupAverage / list.Length;
            return groupAverage;
        }
        delegate void StudentDelegate(Student[] student);
    }
}
