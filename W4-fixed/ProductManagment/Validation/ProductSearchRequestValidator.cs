using FluentValidation;
using ProductManagment.DTOs.Request;

namespace ProductManagment.Validation
{
    public class ProductSearchRequestValidator:AbstractValidator<ProductSearchRequest>
    {
        public ProductSearchRequestValidator() {
            RuleLevelCascadeMode = CascadeMode.Stop;

            RuleFor(x => x.KeyWord)
            .MaximumLength(100).WithMessage("Chỉ được nhập tối đa 100 ký tự")
            .When(x => !string.IsNullOrWhiteSpace(x.KeyWord)).WithMessage("Bạn chưa nhập tên sản phẩm cần tìm");

            RuleFor(x => x.PageIndex)
                .GreaterThanOrEqualTo(1).WithMessage("Số trang phải lớn hơn hoặc bằng 1");
            RuleFor(x => x.PageSize)
                .GreaterThanOrEqualTo(1).WithMessage("Số lượng hiển thị phải lớn hơn 1")
                .LessThanOrEqualTo(100).WithMessage("Sô lượng hiển thị tối đa là 100");



        }
    }
}
