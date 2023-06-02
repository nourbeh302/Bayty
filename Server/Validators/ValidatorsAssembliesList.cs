using Server.Validators.AuthValidators;
using Servers.Validators.AdvertisementValidators;
using System.Reflection;

namespace Server.Validators
{
    public static class ValidatorsAssembliesList
    {
        public static List<Assembly> validatorsAssembly = new List<Assembly>
        {
                typeof(LoginDTOValidator).Assembly,
                typeof(RegisterDTOValidator).Assembly,
                typeof(AdDtoValidator).Assembly,
                typeof(PutAdDtoValidator).Assembly
        };
    }
}
