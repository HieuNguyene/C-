using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week2_day3.IService;
using Week2_day3.model;

namespace Week2_day3.service
{
    internal class StatisticService : IStatistic
    {
        public void PrintTotalStudents(List<Student> students)
        {
            var count = students?.Count ?? 0;
            Console.WriteLine($"Total students: {count}");
        }

        public void PrintStudentsGroupedByFirstLetter(List<Student> students)
        {
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }

            var groups = students
                .Where(s => !string.IsNullOrWhiteSpace(s.Name))
                .GroupBy(s => char.ToUpper(s.Name.Trim()[0]))
                .OrderBy(g => g.Key);

            foreach (var g in groups)
            {
                Console.WriteLine($"{g.Key}: {g.Count()} students");
                foreach (var s in g)
                {
                    Console.WriteLine($"  - {s.Id} | {s.Name}");
                }
            }
        }

        public void PrintDuplicateNames(List<Student> students)
        {
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }

            var duplicates = students
                .Where(s => !string.IsNullOrWhiteSpace(s.Name))
                .GroupBy(s => s.Name.Trim(), StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => new { Name = g.Key, Count = g.Count() })
                .ToList();

            if (!duplicates.Any())
            {
                Console.WriteLine("No duplicate names found.");
                return;
            }

            Console.WriteLine("Duplicate names:");
            foreach (var d in duplicates)
            {
                Console.WriteLine($"{d.Name} - {d.Count} occurrences");
            }
        }

        public void PrintStudentsSortedByName(List<Student> students)
        {
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }

            var ordered = students
                .OrderBy(s => s.Name ?? string.Empty)
                .ToList();

            Console.WriteLine("Students sorted by name:");
            foreach (var s in ordered)
            {
                Console.WriteLine($"{s.Id} | {s.Name}");
            }
        }
    }
}
