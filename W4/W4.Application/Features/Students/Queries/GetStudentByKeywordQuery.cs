using AutoMapper;
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
    }
    public class GetStudentByKeywordQueryHandler : IRequestHandler<GetStudentByKeyWordQuery, ApiResponse<List<StudentResponse>>>
    {
        private readonly ILogger<GetStudentByKeywordQueryHandler> _logger;
        private readonly IStudentRepository _repository;
        private readonly IMapper _mapper;

        public GetStudentByKeywordQueryHandler(ILogger<GetStudentByKeywordQueryHandler> logger, IStudentRepository repository,IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<ApiResponse<List<StudentResponse>>> Handle(GetStudentByKeyWordQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Get all students constain {keyword}", request.Keyword);

            var query = await _repository.GetStudentByKeyWordAsync(request.Keyword, request.PageSize, request.Page);

            var data = _mapper.Map<List<StudentResponse>>(query);
            return new ApiResponse<List<StudentResponse>>()
            {
                Success = true,
                Message = data.Any() ? "Success" : "No student found",
                Data = data
            };

        }
    }

}
