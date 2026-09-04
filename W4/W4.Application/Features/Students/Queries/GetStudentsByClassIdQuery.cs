using MediatR;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs;

namespace W4.Application.Features.Students.Queries
{
    public class GetStudentsByClassIdQuery: IRequest<ApiResponse<List<StudentResponse>>>
    {
        public string ClassId {get;set;} 
        public GetStudentsByClassIdQuery(string classId)
        {
            ClassId = classId;
        }
    }
    public class GetStudentsByClassIdQueryHandler : IRequestHandler<GetStudentsByClassIdQuery, ApiResponse<List<StudentResponse>>>
    {
        private readonly IStudentRepository _studentRepository;

        public GetStudentsByClassIdQueryHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        public async Task<ApiResponse<List<StudentResponse>>> Handle(GetStudentsByClassIdQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetStudentsByClassIdAsync(request.ClassId);
            var response = students.Select(s => new StudentResponse
            {
                Id = s.Id,
                Name = s.Name,
                Dob = s.DateOfBirth,
                Gender = s.Gender,
                ClassId = s.ClassId
            }).ToList();
            return new ApiResponse<List<StudentResponse>> { Success = true, Data = response };
        }
    }
}