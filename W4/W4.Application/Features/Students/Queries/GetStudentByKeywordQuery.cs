using MediatR;
using Microsoft.Extensions.Logging;
using W4.Application.DTOs;
using W4.Application.Interfaces;

namespace W4.Application.Features.Students.Queries
{
    public class GetStudentByKeyWordQuery : IRequest<ApiResponse<List<StudentResponse>>>
    {
        public string? Keyword { get; set; }
        public int PageSize { get; set; } = 10;
        public int Page { get; set; } = 1;

        public GetStudentByKeyWordQuery(string keyWord, int pageSize, int pageNumber)
        {
            Keyword = keyWord;
            PageSize = pageSize;
            Page = pageNumber;
        }
    }
    public class GetStudentByKeywordQueryHandler : IRequestHandler<GetStudentByKeyWordQuery, ApiResponse<List<StudentResponse>>>
    {
        private readonly ILogger<GetStudentByKeywordQueryHandler> _logger;
        private readonly IStudentRepository _repository;

        public GetStudentByKeywordQueryHandler(ILogger<GetStudentByKeywordQueryHandler> logger, IStudentRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        public async Task<ApiResponse<List<StudentResponse>>> Handle(GetStudentByKeyWordQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all students constain {keyword}", request.Keyword);

            var query = await _repository.GetStudentByKeyWordAsync(request.Keyword, request.PageSize, request.Page);

            var data = query.Select(s => new StudentResponse
            {
                Id = s.Id,
                Name = s.Name,
                Dob = s.DateOfBirth,
                Gender = s.Gender,
                ClassId = s.ClassId
            }).ToList();
            return new ApiResponse<List<StudentResponse>>()
            {
                Success = true,
                Message = data.Any() ? "Success" : "No student found",
                Data = data
            };

        }
    }

}
