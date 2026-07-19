using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lession1
{
    internal class Program
    {
        // In danh sach
        static void PrintListStudent(List<Student> list)
        {
            foreach (var student in list)
            {
                student.OutputInfo();
            }
        }
        //Tao sinh vien
        static void CreateStudent(List<Student> list)
        {
            Console.WriteLine("Quantity of Student: ");
            int n;
            while (!int.TryParse(Console.ReadLine(), out n) || n <= 0)
            {
                Console.Write("Quantity is invalid. Try again: ");
            }
            for (int i = 0; i < n; i++)
            {
                Student student = new Student();
                student.InputInfo();
                list.Add(student);
            }

        }
        // Tim sinh vien bang id
        static Student SearchStudentByID(List<Student> list)
        {
            Console.WriteLine("Input student id:");
            int ID = int.Parse(Console.ReadLine());
            Student student = list.FirstOrDefault(s => s.ID==ID);
            if (student == null)
            {
                Console.WriteLine("Student is not found");
                return null;
            }
            student.OutputInfo();
            return student;
        }
        // Cap nhap thong tin sinh vien bang id
        static void UpdateInfoStudent(List<Student> list)
        {
            Student s = SearchStudentByID(list);
            Console.WriteLine("Update name: ");
            string newName= Console.ReadLine();
            s.Name = newName;
        }
        // Xoa sinh vien bang id
        static void DeleteStudentByID(List<Student> list)
        {
            Student s = SearchStudentByID(list);
            list.Remove(s); 
        }
        static void Main(string[] args)
        {
            List<Student> list = new List<Student>();
            while (true)
            {
                Console.WriteLine("1, Create Student");
                Console.WriteLine("2, Display Student");
                Console.WriteLine("3 Search Student by ID");
                Console.WriteLine("4, Update information for Student");
                Console.WriteLine("5, Delete Student");
                Console.WriteLine("6, Exit");
                Console.WriteLine("Enter your choice: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1: CreateStudent(list);break;
                    case 2: PrintListStudent(list);break;
                    case 3: SearchStudentByID(list); break;
                    case 4: UpdateInfoStudent(list);break;
                    case 5: DeleteStudentByID(list);break;
                    case 6: return;
                    default: Console.WriteLine("Your choose is invalid!"); break;
                }
            }
        }
    }
}
            
           
       
    
