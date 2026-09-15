using MediatR;
using W4.Application.Interfaces;
using W4.Domain.Entities;
using W4.Application.DTOs;
using Microsoft.Extensions.Logging;
using AutoMapper;

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
        private readonly IStudentRepository _repository;
        private readonly ILogger<GetStudentsByClassIdQueryHandler> _logger;
        private readonly IMapper _mapper;


        public GetStudentsByClassIdQueryHandler(IStudentRepository studentRepository,ILogger<GetStudentsByClassIdQueryHandler> logger,IMapper mapper)
        {
            _repository = studentRepository;
            _logger = logger;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<StudentResponse>>> Handle(GetStudentsByClassIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Lấy danh sách sinh viên trong lóp {ClassId}",request.ClassId);
            var students = await _repository.GetStudentsByClassIdAsync(request.ClassId);
            var response = _mapper.Map<List<StudentResponse>>(students);
            _logger.LogInformation("Lấy danh sách sinh viên trong lóp {ClassId} thành công!",request.ClassId);
            return new ApiResponse<List<StudentResponse>> { Success = true, Data = response };
        }
    }
}