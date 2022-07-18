using Microsoft.Extensions.Options;

using MySpot.Application.DTO;
using MySpot.Application.Security;
using MySpot.Infrastructure.Auth;
using MySpot.Infrastructure.Time;

namespace MySpot.Tests.Integration.Controllers
{
    public abstract class ControllerTest : IClassFixture<OptionsProvider>
    {
        private readonly IAuthenticator _authenticator;
        protected HttpClient Client { get; }

        protected JwtDto Authorize(Guid userId, string role)
        {
            var jwt = _authenticator.CreateToken(userId, role);
            Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwt.AccessToken);

            return jwt;
        }

        public ControllerTest(OptionsProvider optionsProvider)
        {
            var authOption = optionsProvider.Get<AuthOptions>("auth");
            _authenticator = new Authenticator(new OptionsWrapper<AuthOptions>(authOption), new Clock());
            var app = new MySpotTestApp();
            Client = app.Client;
        }
    }
}
