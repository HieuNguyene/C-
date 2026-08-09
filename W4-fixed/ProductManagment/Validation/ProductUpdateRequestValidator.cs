using FluentValidation;
using ProductManagment.DTOs.Request;

namespace ProductManagment.Validation
{
    public class ProductUpdateRequestValidator:AbstractValidator<ProductUpdateRequest>
    {
        public ProductUpdateRequestValidator() {
            RuleLevelCascadeMode = CascadeMode.Stop;
            RuleFor(x => x.Name)
                            .NotEmpty().WithMessage("Tên không được trống!")
                            .MaximumLength(100).WithMessage("Tên tôi đa chỉ được có 100 ký tự!");

            RuleFor(x => x.Description)
                .MaximumLength(200).WithMessage("Mô tả không được quá 200 ký tự");
            RuleFor(x => x.Price)
                .NotEmpty().WithMessage("Giá không được trống")
                .GreaterThan(0).WithMessage("Giá phải lớn hơn 0");
        }
    }
}
