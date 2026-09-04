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
    public class UpdateScoreValidator : AbstractValidator<UpdateScoreCommand>
    {
        public UpdateScoreValidator()
        {
            // Validate Value (float) - Thang điểm từ 0 đến 10
            RuleFor(x => x.Value)
                .InclusiveBetween(0f, 10f).WithMessage("Điểm số phải nằm trong khoảng từ 0 đến 10!");
        }
    }
}










