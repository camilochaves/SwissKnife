using SwissKnife;

namespace App.Services
{
    public class StartUpServices
    {
        public static void Init(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<TokenTools>();
            services.AddScoped<SecretsHandlerService>(implementationFactory: fac =>
            {
                return new SecretsHandlerService(configuration);
            });
        }
    }
}