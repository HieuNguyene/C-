using W3.DTOs.Request;
using W3.DTOs.Respones;

namespace W3.Interface
{
    public interface IStudentService
    {
        List<StudentRespone> GetAll();
        StudentRespone Create (CreateStudentRequest request);
        StudentRespone? GetById(string id);
        StudentRespone? UpdateById(string id,UpdateStudentRequest request);
        bool DeleteById(string id);
    }
}
