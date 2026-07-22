using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week1.D1;
using Week1.D2;
using Week1.D3_5.model;
using Week1.D3_5.service;
namespace Week1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            int choose;
            while (true)
            {
                Console.WriteLine("1.Day1");
                Console.WriteLine("2.Day2");
                Console.WriteLine("3.Day3-5");
                Console.WriteLine("0.Exit");
                Console.WriteLine("Please,choose");
                while(!int.TryParse(Console.ReadLine(),out choose)){
                    Console.WriteLine("Your choose must be a number!");
                }
                switch (choose)
                {
                    case 0: return;
                    case 1:
                        HelloStudent hello = new HelloStudent();
                        hello.Excute();
                        break;
                    case 2:
                        AverageScore average = new AverageScore();
                        average.Excute();
                        break;
                    case 3:

                        StudentService service = new StudentService();

                        while (true)
                        {
                            Console.WriteLine("1. Create student");
                            Console.WriteLine("2. Print student");
                            Console.WriteLine("3. Update student");
                            Console.WriteLine("4. Search student");
                            Console.WriteLine("5. Delete student");
                            Console.WriteLine("0. Back");

                            while (!int.TryParse(Console.ReadLine(), out choose))
                            {
                                Console.WriteLine("Your choice must be a number!");
                            }

                            switch (choose)
                            {
                                case 0:
                                    goto Exit3;

                                case 1:
                                    service.CreateStudent();
                                    break;

                                case 2:
                                    service.PrintStudent();
                                    break;

                                case 3:
                                    service.UpdateStudentById();
                                    break;

                                case 4:
                                    service.SearchStudentById();
                                    break;

                                case 5:
                                    service.DeleteStudentById();
                                    break;

                                default:
                                    Console.WriteLine("Invalid choice!");
                                    break;
                            }
                        }

                    Exit3:
                        break;
                    default: Console.WriteLine("Your choose is invalid!");break;
                }
            }
            
    }
}
