using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManager_tach_StudentService_.model
{
    internal class Student
    {
        public string id { get; set; }
        public string name { get; set; }

        public void Input()
        {
            Console.WriteLine("Student id: ");
            id = Console.ReadLine();
            Console.WriteLine("Student name: ");
            name = Console.ReadLine();
        }
        public void Output()
        {
            Console.WriteLine($"ID: {id}" );
            Console.WriteLine($"Name: {name}");
        }
    }
}
