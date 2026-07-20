using System;
using System.Collections.Generic;

namespace Week2_day3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stats = new service.StatisticService();
            var studentService = new service.StudentService();

            Console.WriteLine("Loading students...");
            var students = studentService.LoadStudentsSimulatedAsync("data/nts.txt").GetAwaiter().GetResult();
            Console.WriteLine($"Loaded {students.Count} students.");

            while (true)
            {
                Console.WriteLine("--- Student Management ---");
                Console.WriteLine("1. Create student");
                Console.WriteLine("2. Update student");
                Console.WriteLine("3. Delete student");
                Console.WriteLine("4. Print all students");
                Console.WriteLine("5. Search student by ID");
                Console.WriteLine("6. Statistics");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        studentService.CreateStudent(students);
                        break;
                    case "2":
                        studentService.UpdateStudent(students);
                        break;
                    case "3":
                        studentService.DeleteStudent(students);
                        break;
                    case "4":
                        studentService.PrintAll(students);
                        break;
                    case "5":
                        studentService.SearchById(students);
                        break;
                    case "6":
                        ShowStatisticsMenu(students, stats);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }

                Console.WriteLine();
            }
        }

        

        static void ShowStatisticsMenu(List<model.Student> students, service.StatisticService stats)
        {
            while (true)
            {
                Console.WriteLine("--- Statistics ---");
                Console.WriteLine("1. Total students");
                Console.WriteLine("2. Group by first letter");
                Console.WriteLine("3. Duplicate names");
                Console.WriteLine("4. Sorted by name");
                Console.WriteLine("0. Back");
                Console.Write("Choose an option: ");
                var c = Console.ReadLine();
                switch (c)
                {
                    case "1":
                        stats.PrintTotalStudents(students);
                        break;
                    case "2":
                        stats.PrintStudentsGroupedByFirstLetter(students);
                        break;
                    case "3":
                        stats.PrintDuplicateNames(students);
                        break;
                    case "4":
                        stats.PrintStudentsSortedByName(students);
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid option.");
                        break;
                }
                Console.WriteLine();
            }
        }
    }
}
