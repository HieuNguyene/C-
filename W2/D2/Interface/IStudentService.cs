using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week2.D1.model;

namespace Week2.D2.Interface 
{
    interface IStudentService
    {
        Task CreateStudentAsync();
        Task PrintStudentAsync();
        Task <Student?> SearchStudentByIdAsync();
        Task UpdateStudentByIdAsync();
        Task DeleteStudentByIdAsync();
    }
}
