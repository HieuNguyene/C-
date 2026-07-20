using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week2_day3.IService
{
    internal interface IStudentService
    {

        void CreateStudent();
        void UpdateStudent();
        void DeleteStudent();
        void PrintStudent();
        void SearchById();
        Task<List<model.Student>> LoadStudentsAsync(string path);
        Task SaveStudentsAsync(List<model.Student> students, string path);
        
    }
}
