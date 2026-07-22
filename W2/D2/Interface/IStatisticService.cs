using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Week2.D2.Interface
{
    internal interface IStatisticService
    {
        void PrintTotalStudents();

        void PrintStudentsGroupedByFirstLetter();

        void PrintDuplicateNames();


        void PrintStudentsSortedByName();
    }
}
