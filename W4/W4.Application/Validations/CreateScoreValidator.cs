using W4.Application.DTOs;
using W4.Application.Features.Classes.Commands;
using W4.Application.Features.Students.Commands;
using W4.Application.Features.Subjects.Commands;
using W4.Application.Features.Scores.Commands;
using W4.Application.Features.Students.Queries;
using W4.Application.Validations;
using W4.Application.Interfaces;
using FluentValidation;

namespace W4.Application.Validations
{
    public class CreateScoreValidator : AbstractValidator<CreateScoreCommand>
    {
        public CreateScoreValidator()
        {
            // Validate StudentId (Guid)
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Mã học sinh (StudentId) không được để trống hoặc mang giá trị mặc định!");

            // Validate SubjectId (string)
            RuleFor(x => x.SubjectId)
                .NotEmpty().WithMessage("Mã môn học (SubjectId) không được để trống!")
                .MaximumLength(50).WithMessage("Mã môn học không được vượt quá 50 ký tự!");

            // Validate Value (float) - Thang điểm từ 0 đến 10
            RuleFor(x => x.Value)
                .InclusiveBetween(0f, 10f).WithMessage("Điểm số phải nằm trong khoảng từ 0 đến 10!");
        }
    }
}










