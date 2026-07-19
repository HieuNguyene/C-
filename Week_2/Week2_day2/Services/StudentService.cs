using StudentManagement_Interface.Interface;
using StudentManagement_Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace StudentManager_tach_StudentService_.Service
{

    internal class StudentService:IStudentService
    {
        private List<Student> list = new List<Student>();

        // In danh sach
        public void PrintListStudent()
        {
            foreach (var student in list)
            {
                student.Output();
            }
        }
        //Tao sinh vien
        public void CreateStudent()
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
                student.Input();
                list.Add(student);
            }

        }
        // Tim sinh vien bang id
        public Student SearchStudentByID()
        {
            Console.WriteLine("Input student id:");
            string ID = Console.ReadLine();
            Student student = list.FirstOrDefault(s => s.id == ID);
            if (student == null)
            {
                Console.WriteLine("Student is not found");
                return null;
            }
            student.Output();
            return student;
        }
        // Cap nhap thong tin sinh vien bang id
        public void UpdateInfoStudent()
        {
            Student s = SearchStudentByID();
            Console.WriteLine("Update name: ");
            string newName = Console.ReadLine();
            s.name = newName;
        }
        // Xoa sinh vien bang id
        public void DeleteStudentByID()
        {
            Student s = SearchStudentByID();
            list.Remove(s);
        }
    }
}
