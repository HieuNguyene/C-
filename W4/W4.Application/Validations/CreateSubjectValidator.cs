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
    public class CreateSubjectValidator : AbstractValidator<CreateSubjectCommand>
    {
        public CreateSubjectValidator()
        {
            RuleFor(x => x.SubjectId)
                .NotEmpty().WithMessage("Mã môn học không được để trống!")
                .MaximumLength(20).WithMessage("Mã môn học không được vượt quá 20 ký tự!");

            RuleFor(x => x.SubjectName)
                .NotEmpty().WithMessage("Tên môn học không được để trống!")
                .MaximumLength(100).WithMessage("Tên môn học không được vượt quá 100 ký tự!");
        }
    }
}










