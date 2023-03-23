using FluentValidation;
using BaytyAPIs.DTOs.AuthenticationDTOs;

namespace BaytyAPIs.Validators.AuthValidators
{
    public class RegisterDTOValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterDTOValidator()
        {
            RuleFor(r => r.Email)
                .EmailAddress();
        }
    }
}
