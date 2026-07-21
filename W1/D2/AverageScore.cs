using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week1.D2
{   
    internal class AverageScore
    {
        public void Excute()
        {
            Console.WriteLine("Please input your point:");
            float yourPoint;
            while (true)
            {
                if (!float.TryParse(Console.ReadLine(), out yourPoint) || yourPoint < 0 || yourPoint > 10)
                {
                    Console.WriteLine("Your point is invalid,please try again! ");
                }
                else break;
            }

            if (yourPoint < 5.0)
            {
                Console.WriteLine("Fail");
            }
            else if (yourPoint < 7.0)
            {
                Console.WriteLine("Average");
            }
            else if (yourPoint < 8.0)
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
                


