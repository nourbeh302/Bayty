using BaytyAPIs.Validators.AuthValidators;
using System.Reflection;

namespace BaytyAPIs.Validators
{
    public static class ValidatorsAssembliesList
    {
        public static List<Assembly> validatorsAssembly = new List<Assembly>
        {
                typeof(LoginDtoValidator).Assembly,
                typeof(RegisterDtoValidator).Assembly,
        };
    }
}
