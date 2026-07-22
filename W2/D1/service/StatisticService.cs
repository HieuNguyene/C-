using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week2.D2.Interface;
using Week2.D1.model;
namespace Week2.D1.service
{
    internal class StatisticService : IStatisticService
    {
        List<Student> students = new List<Student>();
        public void PrintTotalStudents()
        {
            var total = students.Count;
            Console.WriteLine($"Tong so hoc sinh: {total}");
        }

        public void PrintStudentsGroupedByFirstLetter()
        {
            var results = students
                        .Where(s => !string.IsNullOrWhiteSpace(s.Name))
                        .GroupBy(s => char.ToUpper(s.Name[0]))
                        .OrderBy(g => g.Key);
            foreach (var student in results)
            {
                student.ToString();
            }

        }

        public void PrintDuplicateNames()
        {
            var results = students
                    .GroupBy(s => s.Name)
                    .Where(g => g.Count() > 1);
            foreach (var g in students)
            {
                g.ToString();
            }
        }


        public void PrintStudentsSortedByName()
        {
            var results = students.GroupBy(s => s.Name);
            foreach (var student in results)
            {
                student.ToString();
            }
        }
    }
}
