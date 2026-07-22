using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week2.D1.model;
using Week2.D2.Interface;
namespace Week2.D1.service
{
    internal class StudentService:IStudentService
    {
        List<Student> students = new List<Student>();
        // Tao danh sach sinh vien
        public async Task CreateStudentAsync()
        {
            int quantity;
            while (true)
            {
                Console.WriteLine("Input quantity of students: ");
                if (!int.TryParse(Console.ReadLine(), out quantity) || quantity <= 0)
                {
                    Console.WriteLine("Quantity is invalid!Please try again");
                }
                else break;
            }
            for (var i =0; i < quantity; i++)
            {
                Student s = new Student();
                s.InPutStudent();
                students.Add(s);
            }
            await Task.Delay(2000);
            Console.WriteLine("Updated list student!");
        }
        // In danh sach sinh vien
        public async Task PrintStudentAsync()
        {
            foreach (var s in students)
            {
                await Task.Delay(500);
                Console.WriteLine(s.ToString());
            }
        }
        //Tim sinh vien bang ID
        public async Task<Student?> SearchStudentByIdAsync()
        {
            string? id="";
            while (string.IsNullOrWhiteSpace(id))
            {
                Console.WriteLine("Input ID need search:");
                id = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(id))
                {
                    Console.WriteLine("ID is invalid,please try again");
                }
            }
            foreach (var s in students)
            {
                if (s.Id == id)
                {
                    await Task.Delay(2000);
                    Console.WriteLine($"Current: {s.ToString()}");
                    return s;
                }
                    
            }
            Console.WriteLine($"Not found student match {id}");
            return null;
        }
        // Cap nhat danh sach sinh vien bang id
        public async Task UpdateStudentByIdAsync()
        {
            Student? student =await SearchStudentByIdAsync();
            if (student != null)
            {
                Console.WriteLine("Please update information: ");
                student.InPutStudent();
                await Task.Delay(2000);
                Console.WriteLine("Updated!");
            }
            
        }
        // Xoa sinh vien bang id
        public async Task DeleteStudentByIdAsync()
        {
            Student? student = await SearchStudentByIdAsync();
            if (student != null)
            {

                char answer;
                while (true)
                {
                    Console.WriteLine("Do you want delete this student(Y/N) ?");
                    if (!char.TryParse(Console.ReadLine(), out answer))
                    {
                        Console.WriteLine("Please input Y or N");
                        continue;
                    }
                    answer = char.ToUpper(answer);
                    if (answer =='Y' || answer == 'N')
                    {
                        break;
                    }
                    Console.WriteLine("Please input Y or N");
                }
                
                if (answer == 'Y')
                {
                    students.Remove(student);
                    await Task.Delay(2000);
                    Console.WriteLine("Deleted!");
                }
                else 
                {
                    return;
                }   
            }
            
        }
    }
}
