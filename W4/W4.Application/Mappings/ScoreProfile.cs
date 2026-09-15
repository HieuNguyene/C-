using AutoMapper;
using W4.Application.DTOs.Responses;
using W4.Application.Features.Scores.Commands;
using W4.Domain.Entities;

namespace W4.Application.Mappings
{
    public class ScoreProfile : Profile
    {
        public ScoreProfile()
        {
            // Chiều ra: Entity -> DTO
            CreateMap<Score, ScoreResponse>();

            // Chiều vào: Command -> Entity
            CreateMap<CreateScoreCommand, Score>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Student, opt => opt.Ignore())
                .ForMember(dest => dest.Subject, opt => opt.Ignore());
        }
    }
}
