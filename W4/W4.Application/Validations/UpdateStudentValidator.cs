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
    public class UpdateStudentValidator : AbstractValidator<UpdateStudentCommand>
    {
        public UpdateStudentValidator()
        {
            ClassLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Name is required!").MinimumLength(3).WithMessage("Tên tối thiểu phải có 3 ký tự");
            RuleFor(x => x.Dob).LessThan(DateTime.Now).WithMessage("Ngày sinh phải nhỏ hơn hiện tại");
            RuleFor(x => x.Gender).IsInEnum().WithMessage("Giới tính không hợp lệ!");

            When(x => !string.IsNullOrEmpty(x.ClassId), () =>
            {
                RuleFor(x => x.ClassId)
                    .NotEmpty().WithMessage("ClassId không được để trống nếu có truyền!");
            });
        }
    }
}










