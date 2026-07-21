using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1.D1
{
    
    internal class HelloStudent
    {
        public void Excute()
        {
            Console.WriteLine("Welcome, please fill in your information!");
            Console.WriteLine("Your name:");
            string? name;
            do
            {
                name = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(name));

            Console.WriteLine("Your student id:");
            string? msv = Console.ReadLine();
            Console.WriteLine("Your age:");
            int age;
            while (true)
            {
                var ageInput = Console.ReadLine();
                if (!int.TryParse(ageInput, out age) || age <= 0 || age >= 100)
                {
                    Console.WriteLine("Your age invalid, please try again!");
                }
                else break;
            }
            Console.WriteLine($"Welcome {name}-{msv}-{age}");
        }
    }
}
