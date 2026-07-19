using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tinhdiemtrungbinh
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input your point:");
            float yourPoint = float.Parse(Console.ReadLine());
            while (true)
            {
                if (yourPoint < 0 || yourPoint > 10)
                {
                    Console.WriteLine("Your point is invalid,please try again! ");
                    yourPoint = float.Parse(Console.ReadLine());
                }
                else break;
            }

            if(yourPoint < 5.0)
            {
                Console.WriteLine("Fail");
            }
            else if(yourPoint <7.0)
            {
                Console.WriteLine("Average");
            }
            else if(yourPoint <8.0)
            {
                Console.WriteLine("Good");
            }
            else
            {
                Console.WriteLine("Very good");
            }

        }
    }
}
