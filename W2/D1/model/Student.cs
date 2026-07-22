using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2.D1.model
{
    internal class Student
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public override string ToString(){
            return $"ID: {Id}- Name: {Name}";
        }
        public void InPutStudent()
        {
            do
            {
                Console.WriteLine("Input student id: ");
                Id = Console.ReadLine() ?? "";
            } while (string.IsNullOrWhiteSpace(Id));
            do
            {
                Console.WriteLine("Input student name: ");
                Name = Console.ReadLine() ?? "";
            }while (string.IsNullOrWhiteSpace(Name));
        }
    }
}
