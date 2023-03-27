using FluentValidation;
using BaytyAPIs.Dtos.AuthenticationDtos;

namespace BaytyAPIs.Validators.AuthValidators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.Email)
                .EmailAddress();
        }
    }
}
