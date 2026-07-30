using FluentValidation;
using W3.DTOs.Request;

namespace W4.FluentValidation
{
    public class CreateStudentValidator: AbstractValidator<CreateStudentRequest>
    {
        public CreateStudentValidator() 
        {
            RuleFor(x => x.Name).NotEmpty()
                .WithMessage("Name is required!").MinimumLength(3).WithMessage("Name must be least 3 character");
        }
    }
}
