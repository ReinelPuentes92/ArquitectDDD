using Iktan.Ecommerce.App.Interface;
using Iktan.Ecommerce.App.Main;
using Iktan.Ecommerce.Domain.Core;
using Iktan.Ecommerce.Domain.Interface;
using Iktan.Ecommerce.Infraestructure.Data;
using Iktan.Ecommerce.Infraestructure.Interface;
using Iktan.Ecommerce.Infraestructure.Repository;
using Iktan.Ecommerce.Transversal.Common;
using Iktan.Ecommerce.Transversal.Logging;
using System.Runtime.CompilerServices;

namespace Iktan.Ecommerce.Service.WebAPI.Modules.Injection
{
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            //dependency injection
            //AddSingleton => Se instancia una sola vez y se reutiliza en los demas llamados
            //builder.Services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();

            //AddScoped => Se instancia una sola vez por solicitud
            services.AddScoped<ICustomerApplication, CustomerApplication>();
            services.AddScoped<ICustomerDomain, CustomerDomain>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserApplication, UserApplication>();
            services.AddScoped<IUserDomain, UserDomain>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
