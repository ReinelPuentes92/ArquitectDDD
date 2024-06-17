namespace Iktan.Ecommerce.Service.WebAPI.Modules.Feature
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration, string policyName)
        {
            var configCORS = configuration.GetValue<string>("Configs:OriginCors");

            //CORS
            services.AddCors(options => options.AddPolicy(policyName, builder => builder.WithOrigins(configCORS)
                                                                                                     .AllowAnyHeader()
                                                                                                     .AllowAnyMethod()));

            return services;
        }
    }
}
