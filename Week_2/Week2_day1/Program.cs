using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement_Interface.Interface;
using StudentManager_tach_StudentService_.Service;

namespace StudentManagement_Interface
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IStudentService service = new StudentService();

            int selection;
            while (true)
            {
                Console.WriteLine("\n1,Create Student" +
                    "\n2,Print list student" +
                    "\n3,Search student by id" +
                    "\n4,Update student by id" +
                    "\n5,Delete student bt id" +
                    "\n6,Exit" +
                    "\n Please, choose your selection: ");

                while (!int.TryParse(Console.ReadLine(), out selection))
                {
                    Console.WriteLine("Your selection is invalid! Please try again: ");
                }

                switch (selection)
                {
                    default: Console.WriteLine("Your selection is invalid!"); break;
                    case 1: service.CreateStudent(); break;
                    case 3: service.SearchStudentByID(); break;
                    case 2: service.PrintListStudent(); break;
                    case 4: service.UpdateInfoStudent(); break;
                    case 5: service.DeleteStudentByID(); break;
                    case 6: return;
                }
            }
        }
    }
}
