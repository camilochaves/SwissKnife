using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SwissKnife;

namespace App.Authentication
{
    public class StartUpAuthentication
    {
        public static void Init(
            IServiceCollection services)
        {
            var provider = services.BuildServiceProvider();
            var scope = provider.CreateScope();
            var secretsHandler = scope.ServiceProvider.GetRequiredService<SecretsHandlerService>();
            services.AddAuthentication(
                options =>
                {
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                }
                )
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = secretsHandler.GetFromConfig("JWT:issuer"),
                        ValidAudience = secretsHandler.GetFromConfig("JWT:audience"),
                        ValidateIssuer = true,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = true,
                        IssuerSigningKey =
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(
                                        secretsHandler.GetFromConfig("JWT:secretKey")
                                    )
                                ),
                    };
                    options.Events = new JwtBearerEvents()
                    {
                        OnTokenValidated = context =>
                        {
                            context.HttpContext.User = context.Principal;
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = options =>
                        {
                            options.Response.WriteAsJsonAsync(new { error = " Authentication Failed!" });
                            return Task.CompletedTask;
                        },
                        OnForbidden = options =>
                        {
                            options.Response.WriteAsJsonAsync(new { error = "Forbidden!" });
                            return Task.CompletedTask;
                        },
                        OnMessageReceived = options =>
                        {
                            Console.WriteLine("OnMessageReceived Called! ");
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}