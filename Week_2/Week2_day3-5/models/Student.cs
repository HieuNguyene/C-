using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_day3.models
{
    public class Student
    {
        public string Id {  get; set; }
        public string Name { get; set; }

        public void OutPut()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Name: {Name}");
        }
        public void InPut()
        {
            Console.WriteLine("Input id: ");
            Id= Console.ReadLine();
            Console.WriteLine("Input name: ");
            Name = Console.ReadLine();
        }
    }
}
