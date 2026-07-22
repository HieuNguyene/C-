using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week2.D1.model;
using Week2.D1.service;
using Week2.D2.Interface;
namespace Week1
{
    internal class Program
    {

        static async Task Main(string[] args)
        {
        int choose;
        StudentService service = new StudentService();

        while (true)
        {
          Console.WriteLine("1. Create student");
          Console.WriteLine("2. Print student");
          Console.WriteLine("3. Update student");
          Console.WriteLine("4. Search student");
          Console.WriteLine("5. Delete student");
          Console.WriteLine("0. Back");
          Console.WriteLine("Your choose: ");
        while (!int.TryParse(Console.ReadLine(), out choose))
        {
            Console.WriteLine("Your choice must be a number!");
        }
                switch (choose)
         {
            case 0:return;
            case 1:
                await service.CreateStudentAsync();
                break;

            case 2:
                await service.PrintStudentAsync();
                break;

            case 3:
                await service.UpdateStudentByIdAsync();
                break;

             case 4:
                await service.SearchStudentByIdAsync();
                break;

             case 5:
                await service.DeleteStudentByIdAsync();
                break;

             default:
                Console.WriteLine("Invalid choice!");
                break;
         }
        }

        }
    }
}

