using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week2_day3.model;
namespace Week2_day3.IService
{
    internal interface IStatistic
    {
        void PrintTotalStudents(List<Student> students);

        void PrintStudentsGroupedByFirstLetter(List<Student> students);

        void PrintDuplicateNames(List<Student> students);

       
        void PrintStudentsSortedByName(List<Student> students);
    }
}
