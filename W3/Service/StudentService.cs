using System.Reflection.Metadata.Ecma335;
using W3.DTOs.Request;
using W3.DTOs.Respones;
using W3.Interface;
using W3.model;
using W3.Responses;

namespace W3.Service
{
    public class StudentService:IStudentService
    {
        private static List<Student> students = new List<Student>();          
        public ApiResponse<List<StudentResponse>> GetAll(StudentQueryRequest request)
        {
            var querry = students.AsQueryable();
            if (!string.IsNullOrWhiteSpace(request.Keyword)) 
            {
                querry = querry
                .Where(s => s.Name.Contains(request.Keyword, StringComparison.OrdinalIgnoreCase));
            }
            var data = querry
                .Skip((request.Page - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(s => new StudentResponse
                {
                    Id = s.Id,
                    Name = s.Name,
                }).ToList();
            return  new ApiResponse<List<StudentResponse>>()
            {
                Success = true,
                Message = data.Any()?"Success":"No student found",
                Data = data 
            };
            
        }
        public ApiResponse<StudentResponse> Create(CreateStudentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ApiResponse<StudentResponse>()
                {
                    Success = false,
                    Message = "Name is not empty or null",
                };
            }
            Student student = new Student()
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            };
            students.Add(student);
            return new ApiResponse<StudentResponse>(){
                Success = true,
                Message ="Success",
                Data = new StudentResponse() {
                    Id = student.Id,
                    Name = request.Name
                }
            };
        }
        public ApiResponse<StudentResponse> GetById(Guid id)
        {
            Student? student = students.SingleOrDefault(s => s.Id == id);
            if(student == null)
            {
                return new ApiResponse<StudentResponse>
                {
                    Success = false,
                    Message = "fail",
                    Data = null
                };
            }
            return new ApiResponse<StudentResponse>
            {
                Success = true,
                Message = "Success",
                Data = new StudentResponse{
                    Id = student.Id,
                    Name = student.Name
                }
            };
        }
        public ApiResponse<bool> UpdateById(Guid id,UpdateStudentRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
            {
                return new ApiResponse<bool>()
                {
                    Success = false,
                    Message = "Name is not empty or null",
                    Data = false
                };
            }
            Student? student = students.SingleOrDefault(s => s.Id == id);
            if (student == null)
            {
                return new ApiResponse<bool>()
                {
                    Success = false,
                    Message = "Fail",
                };
            }
            student.Name = request.Name;
            return new ApiResponse<bool>()
            {
                Success=true,
                Message ="Success",
            };
        }
        public ApiResponse<bool> DeleteById(Guid id) 
        {
            Student? student = students.SingleOrDefault(s => s.Id == id);
            if (student == null) 
            {
                return new ApiResponse<bool>()
                {
                    Success = false,
                    Message = "Fail",
                };
            }
            students.Remove(student);
            return new ApiResponse<bool>()
            {
                Success = true,
                Message = "Fail",
            };
        }
    }
}
