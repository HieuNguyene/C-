using MediatR;
using W4.Application.DTOs;

namespace W4.Application.Features.Students.Queries
{
    public class GetStudentByKeyWord : IRequest<ApiResponse<List<StudentResponse>>>
    {
        
    }
}