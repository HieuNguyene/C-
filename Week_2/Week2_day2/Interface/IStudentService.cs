using StudentManagement_Interface.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement_Interface.Interface
{
    internal interface IStudentService
    {
         void PrintListStudent();
         void CreateStudent();
         Student SearchStudentByID();
         void DeleteStudentByID();
        void UpdateInfoStudent();
    }
}
