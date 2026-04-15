using FluentValidation;
using Project.DAL;

namespace Project.BLL
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryUpdateValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required")
                .MaximumLength(100).WithMessage("Category name cannot exceed 100 characters")
                .MustAsync(CheckNameIsUnique).WithMessage("Category name must be unique");
        }
        private async Task<bool> CheckNameIsUnique(string name, CancellationToken cancellationToken)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetAllAsync();
            var isUnique = existingProduct.Any(p => p.Name == name);
            return !isUnique;
        }
    }
}

