using FluentValidation;
using BaytyAPIs.Dtos.AuthenticationDtos;

namespace BaytyAPIs.Validators.AuthValidators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            //RuleFor(l => l.Email)
            //    .EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible)
            //    .NotEmpty()
            //    .NotNull();

            RuleFor(l => l.Password)
                .NotEmpty()
                .NotNull()
                .MaximumLength(60)
                .MinimumLength(8);
        }
    }
}
