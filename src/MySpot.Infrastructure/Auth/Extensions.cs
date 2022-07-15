using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

using MySpot.Application.Security;

using System.Text;

namespace MySpot.Infrastructure.Auth
{
    internal static class Extensions
    {
        private const string OptionSectionName = "auth";

        public static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration)
        {
            var options = configuration.GetOptions<AuthOptions>(OptionSectionName);

            services
                .Configure<AuthOptions>(configuration.GetRequiredSection(OptionSectionName))
                .AddSingleton<IAuthenticator, Authenticator>()
                .AddSingleton<ITokenStorage, TokenStorage>()
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(o =>
                {
                    o.Audience = options.Audience;
                    o.IncludeErrorDetails = true;
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidIssuer = options.Issuer,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SigningKey))
                    };
                });

            return services;
        }
    }
}
