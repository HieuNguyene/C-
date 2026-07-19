using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement_Interface.Models
{
    internal class Student
    {
        public string id {  get; set; }
        public string name { get; set; }
        public void Output()
        {
            Console.WriteLine($"ID: {id}");
            Console.WriteLine($"Name: {name}");
        }
        public void Input()
        {
            Console.WriteLine("Input id: ");
            id = Console.ReadLine();
            Console.WriteLine("Input name: ");
            name = Console.ReadLine();
        }
    }
}
