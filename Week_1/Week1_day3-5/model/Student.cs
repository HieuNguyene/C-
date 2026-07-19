using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lession1
{
    internal class Student
    {
        public int ID { get; set;  }
        public string Name { get; set; }

        public void InputInfo()
        {
            Console.WriteLine("Enter ID: ");
            ID = int.Parse(Console.ReadLine());
            Console.WriteLine("Enter Name: ");
            Name = Console.ReadLine();
        }
        public void OutputInfo() {
            Console.WriteLine($"ID: {ID}, Name: {Name}");
        }
    }
}
