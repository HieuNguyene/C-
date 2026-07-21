using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week1.D3_5.model;
namespace Week1.D3_5.service
{
    internal class StudentService
    {
        List<Student> students = new List<Student>();
        // Tao danh sach sinh vien
        public void CreateStudent()
        {
            int quantity;
            while (true)
            {
                Console.WriteLine("Input quantity of students: ");
                if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Quantity is invalid!Please try again");
                }
                else break;
            }
            for (var i =0; i < quantity; i++)
            {
                Student s = new Student();
                s.InPutStudent();
                students.Add(s);
            }
            Console.WriteLine("Updated list student!");
        }
        // In danh sach sinh vien
        public void PrintStudent()
        {
            foreach (var s in students)
            {
                s.ToString();
            }
        }
        //Tim sinh vien bang ID
        public Student? SearchStudentById()
        {
            string? id="";
            while (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Input ID need search:");
                id = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("ID is invalid,please try again");
                }
            }
            foreach (var s in students)
            {
                if (s.Id == id)
                    return s;
            }
            Console.WriteLine($"Not found student match {id}");
            return null;
        }
        // Cap nhat danh sach sinh vien bang id
        public void UpdateStudentById()
        {
            Student? student = SearchStudentById();
            if (student != null)
            {
                Console.WriteLine("Please update information: ");
                Console.WriteLine($"Current: {student.ToString()}");
                student.InPutStudent();
                Console.WriteLine("Updated!");
            }
            else
            {
                Console.WriteLine("Not found student!");
            }
        }
        // Xoa sinh vien bang id
        public void DeleteStudentById()
        {
            Student? student = SearchStudentById();
            if (student != null)
            {
                Console.WriteLine($"Current: {student.ToString()}");
                char answer;
                while (true)
                {
                    Console.WriteLine("Do you want delete this student(Y/N) ?");
                    if (char.TryParse(Console.ReadLine(), out answer))
                    {
                        Console.WriteLine("Please input Y or N");
                    }
                    else break;
                }
                if (answer.ToString().ToUpper() == "Y")
                {
                    students.Remove(student);
                    Console.WriteLine("Deleted!");
                }
                else
                {
                    return;
                }
            }
            else
            {
                Console.WriteLine("Not found student!");
            }
        }
    }
}
