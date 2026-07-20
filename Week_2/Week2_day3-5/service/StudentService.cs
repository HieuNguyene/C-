using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week2_day3.model;
using System.IO;


namespace Week2_day3.service
{
    internal class StudentService
    {
        public void CreateStudent(List<Student> students)
        {
            Console.Write("Input id: ");
            var id = Console.ReadLine();
            Console.Write("Input name: ");
            var name = Console.ReadLine();
            students.Add(new Student { Id = id, Name = name });
            Console.WriteLine("Student added.");
        }

        public void UpdateStudent(List<Student> students)
        {
            Console.Write("Input id to update: ");
            var id = Console.ReadLine();
            var s = students.Find(x => x.Id == id);
            if (s == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }
            Console.Write($"New name (current: {s.Name}): ");
            var newName = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(newName)) s.Name = newName;
            Console.WriteLine("Student updated.");
        }

        public void DeleteStudent(List<Student> students)
        {
            Console.Write("Input id to delete: ");
            var id = Console.ReadLine();
            var s = students.Find(x => x.Id == id);
            if (s == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }
            students.Remove(s);
            Console.WriteLine("Student removed.");
        }

        public void PrintAll(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("No students available.");
                return;
            }
            Console.WriteLine("All students:");
            foreach (var s in students)
            {
                Console.WriteLine($"{s.Id} | {s.Name}");
            }
        }

        public void SearchById(List<Student> students)
        {
            Console.Write("Input id to search: ");
            var id = Console.ReadLine();
            var s = students.Find(x => x.Id == id);
            if (s == null)
            {
                Console.WriteLine("Student not found.");
                return;
            }
            Console.WriteLine($"Found: {s.Id} | {s.Name}");
        }

       
        public async Task<List<Student>> LoadStudentsAsync(string path)
        {
            var list = new List<Student>();
            if (!File.Exists(path)) return list;

            using (var sr = new StreamReader(path))
            {
                string content = await sr.ReadToEndAsync().ConfigureAwait(false);
                var lines = content.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length >= 2)
                    {
                        list.Add(new Student { Id = parts[0], Name = parts[1] });
                    }
                }
            }

            return list;
        }

       
        public async Task<List<Student>> LoadStudentsSimulatedAsync(string path)
        {
            if (File.Exists(path))
            {
                return await LoadStudentsAsync(path).ConfigureAwait(false);
            }

            
            await Task.Delay(800).ConfigureAwait(false);

            return new List<Student>
            {
                new Student { Id = "1", Name = "Binh" },
                new Student { Id = "2", Name = "An" },
                new Student { Id = "3", Name = "Chi" }
            };
        }

        public async Task SaveStudentsAsync(List<Student> students, string path)
        {
            var dir = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var sw = new StreamWriter(path, false))
            {
                foreach (var s in students)
                {
                    var line = $"{s.Id}|{s.Name}";
                    await sw.WriteLineAsync(line).ConfigureAwait(false);
                }
            }
        }
    }
}
