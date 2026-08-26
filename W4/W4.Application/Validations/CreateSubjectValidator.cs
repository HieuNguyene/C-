using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Infrastructure.Repositories.Interfaces;
using FluentValidation;
using W4.Application.DTOs;

namespace W4.Application.Validations
{
    public class CreateSubjectValidator : AbstractValidator<CreateSubjectRequest>
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









