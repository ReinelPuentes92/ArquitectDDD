using Microsoft.Extensions.DependencyInjection;
using Iktan.Ecommerce.App.Validation;

namespace Iktan.Ecommerce.Service.WebAPI.Modules.Validator
{
    public static class ValidatorExtensions
    {
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            // AddTransient = se crea una nueva instacia de validador por cada petecion
            services.AddTransient<UserDtoValidator>();

            return services;
        }
    }
}
