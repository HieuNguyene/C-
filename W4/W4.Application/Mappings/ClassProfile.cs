using AutoMapper;
using W4.Application.DTOs.Responses;
using W4.Application.Features.Classes.Commands;
using W4.Domain.Entities;

namespace W4.Application.Mappings
{
    public class ClassProfile : Profile
    {
        public ClassProfile(){
            // Đầu ra
            CreateMap<Class,ClassResponse>();
            // Đầu vào
            CreateMap<CreateClassCommand,Class>()
                .ForMember(dest => dest.Students, opt => opt.Ignore());
        }
    }
}