using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloStudent
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome,please fill in your information! ");
            Console.WriteLine("Your name:");
            string name;
            while (true)
            {
                name = Console.ReadLine();
                if (name.Trim().Length == 0)
                {
                    Console.WriteLine("Your name cannot be empty,please try again! ");
                }
                else break;
            }
            Console.WriteLine("Your student id:");
            string msv = Console.ReadLine();
            Console.WriteLine("Your age:");
            int age;
            while (true)
            {
                age= int.Parse(Console.ReadLine());
                if (age <=0 || age >=100)
                {
                    Console.WriteLine("Your age invalid,please try again! ");
                }
                else break;
            }
            Console.WriteLine($"Name: {name}");
            Console.WriteLine($"ID: {msv}");
            Console.WriteLine($"Age: {age}");
        }
    }
}
