using FluentValidation;
using Project.DAL;

namespace Project.BLL
{
    public class CategoryCreateValidator:AbstractValidator<CategoryCreateDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryCreateValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.")
                .MustAsync(CheckNameIsUnique).WithMessage("Category name must be unique.");
        }
        private async Task<bool> CheckNameIsUnique(string name, CancellationToken cancellationToken)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetAllAsync();
            var isUnique = existingProduct.Any(p => p.Name == name);
            return !isUnique;
        }
    }
}
