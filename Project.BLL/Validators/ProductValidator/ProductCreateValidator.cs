using FluentValidation;
using Project.DAL;

namespace Project.BLL
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductCreateValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Product name is required.")
                .MaximumLength(100).WithMessage("Product name must not exceed 100 characters.")
                .MustAsync(CheckNameIsUnique).WithMessage("Product name must be unique.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Product description must not exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Product price must be greater than zero.");

            RuleFor(x => x.StockQuantity)
                .GreaterThanOrEqualTo(0).WithMessage("Stock quantity cannot be negative.");

        }

        private async Task<bool> CheckNameIsUnique(string name, CancellationToken cancellationToken)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetAllAsync();
            var isUnique = existingProduct.Any(p => p.Name == name);
            return !isUnique;
        }
    }
}
