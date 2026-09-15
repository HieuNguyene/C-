using AutoMapper;
using W4.Application.DTOs;
using W4.Application.Features.Students.Commands;
using W4.Domain.Entities;

namespace W4.Application.Mappings
{
    public class StudentProfile : Profile
    {
        public StudentProfile()
        {
            // Chiều ra: Entity -> DTO
            CreateMap<Student, StudentResponse>();

            // Chiều vào: Command -> Entity
            CreateMap<CreateStudentCommand, Student>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Class, opt => opt.Ignore())
                .ForMember(dest => dest.Scores, opt => opt.Ignore());
        }
    }
}