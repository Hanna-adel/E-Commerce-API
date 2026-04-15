using FluentValidation;
using Project.BLL;
using Project.DAL;

public class OrderCreateValidator : AbstractValidator<OrderCreateDto>
{
    private readonly IUnitOfWork _unitOfWork;
    public OrderCreateValidator(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;

        RuleFor(x => x.ShippingAddress)
            .NotEmpty().WithMessage("Shipping address is required")
            .MaximumLength(500).WithMessage("Shipping address cannot exceed 500 characters");
        _unitOfWork = unitOfWork;
    }
}