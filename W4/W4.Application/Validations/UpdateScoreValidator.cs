using W4.Application.DTOs;
using W4.Application.Validations;
using W4.Application.Interfaces;
using FluentValidation;
using W4.Application.DTOs;

namespace W4.Application.Validations
{
    public class UpdateScoreValidator : AbstractValidator<UpdateScoreRequest>
    {
        public UpdateScoreValidator()
        {
            // Validate Value (float) - Thang điểm từ 0 đến 10
            RuleFor(x => x.Value)
                .InclusiveBetween(0f, 10f).WithMessage("Điểm số phải nằm trong khoảng từ 0 đến 10!");
        }
    }
}









