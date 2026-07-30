using FluentValidation;
using W3.DTOs.Request;

namespace W4.FluentValidation
{
    public class UpdateStudentValidator :AbstractValidator<UpdateStudentRequest>
    {
        public UpdateStudentValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is not empty")
                .MinimumLength(3)
                .WithMessage("Name must be least 3 character");
        }
    }
}
