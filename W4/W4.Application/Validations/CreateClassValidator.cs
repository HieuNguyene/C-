using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Application.Interfaces;
using FluentValidation;
using W4.Application.DTOs;

namespace W4.Application.Validations
{
    public class CreateClassValidator : AbstractValidator<CreateClassRequest>
    {
        public CreateClassValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.ClassId)
                .NotEmpty().WithMessage("Mã lớp (ClassId) không được để trống!")
                .MaximumLength(50).WithMessage("Mã lớp không được vượt quá 50 ký tự.");

            RuleFor(x => x.ClassName)
                .NotEmpty().WithMessage("Tên lớp (ClassName) không được để trống!")
                .MinimumLength(3).WithMessage("Tên lớp nên có ít nhất 3 ký tự.")
                .MaximumLength(200).WithMessage("Tên lớp không được vượt quá 200 ký tự.");
        }
    }
}









