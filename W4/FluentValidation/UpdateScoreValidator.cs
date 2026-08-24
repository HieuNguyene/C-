using FluentValidation;
using W4.Service.DTOs;

namespace W4.FluentValidation
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


