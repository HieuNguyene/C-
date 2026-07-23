using System.Reflection.Metadata.Ecma335;
using W3.DTOs.Request;
using W3.DTOs.Respones;
using W3.Interface;
using W3.model;

namespace W3.Service
{
    public class StudentService:IStudentService
    {
        private static List<Student> students = new List<Student>();          
        public List<StudentRespone> GetAll()
        {
            return students.Select(s => new StudentRespone
            {
                Id = s.Id,
                Name = s.Name
            }).ToList();
        }
        public StudentRespone Create(CreateStudentRequest request)
        {
            Student student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            students.Add(student);
            return new StudentRespone{
                Id = student.Id,
                Name = request.Name
            };
        }
        public StudentRespone? GetById(string id)
        {
            Student? student = students.SingleOrDefault(s => s.Id == Guid.Parse(id));
            
            if(student == null)
            {
                return null;
            }
            return new StudentRespone
            {
                Id = student.Id,
                Name = student.Name
            };
        }
        public StudentRespone? UpdateById(string id,UpdateStudentRequest request)
        {
            Student? student = students.SingleOrDefault(s => s.Id == Guid.Parse(id));
            if (student == null) 
            {
                return null;
            }
            student.Name = request.Name;
            return new StudentRespone
            {
                Id = student.Id,
                Name = request.Name
            };
        }
        public bool DeleteById(string id) 
        {
            Student? student = students.SingleOrDefault(s => s.Id == Guid.Parse(id));
            if (student == null) 
            {
                return false;
            }
            return students.Remove(student);
        }
    }
}
