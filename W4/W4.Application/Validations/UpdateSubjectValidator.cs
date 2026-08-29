using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Application.Interfaces;
using FluentValidation;
using W4.Application.DTOs;

namespace W4.Application.Validations
{
    public class UpdateSubjectValidator : AbstractValidator<UpdateSubjectRequest>
    {
        public UpdateSubjectValidator()
        {
            RuleFor(x => x.SubjectName)
                .NotEmpty().WithMessage("Tên môn học không được để trống!")
                .MaximumLength(100).WithMessage("Tên môn học không được vượt quá 100 ký tự!");
        }
    }
}









