using FluentValidation;
using W4.Service.DTOs;

namespace W4.FluentValidation
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


