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
    public class UpdateSubjectValidator : AbstractValidator<UpdateSubjectCommand>
    {
        public UpdateSubjectValidator()
        {
            RuleFor(x => x.SubjectName)
                .NotEmpty().WithMessage("Tên môn học không được để trống!")
                .MaximumLength(100).WithMessage("Tên môn học không được vượt quá 100 ký tự!");
        }
    }
}










