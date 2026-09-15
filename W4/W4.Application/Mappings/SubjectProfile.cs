using AutoMapper;
using W4.Application.DTOs.Responses;
using W4.Application.Features.Subjects.Commands;
using W4.Domain.Entities;

namespace W4.Application.Mappings
{
    public class SubjectProfile : Profile
    {
        public SubjectProfile()
        {
            // Chiều ra: Entity -> DTO
            CreateMap<Subject, SubjectResponse>();

            // Chiều vào: Command -> Entity
            CreateMap<CreateSubjectCommand, Subject>()
                .ForMember(dest => dest.Scores, opt => opt.Ignore());
        }
    }
}
