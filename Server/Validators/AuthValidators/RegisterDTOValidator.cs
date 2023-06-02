using FluentValidation;
using Server.DTOs.AuthenticationDTOs;

namespace Server.Validators.AuthValidators
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
