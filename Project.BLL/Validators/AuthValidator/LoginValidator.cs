using FluentValidation;
using Project.DAL;

namespace Project.BLL
{
    public class LoginValidator:AbstractValidator<LoginDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        public LoginValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is required.")
                .EmailAddress()
                .WithMessage("Please enter a valid email address.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password is required"); 
        }
    }
}
