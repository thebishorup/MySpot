using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using MySpot.Application.Command;
using MySpot.Application.DTO;
using MySpot.Core.Entities;
using MySpot.Core.ValueObjects;
using MySpot.Infrastructure.Security;
using MySpot.Infrastructure.Time;

using Shouldly;

using System.Net;
using System.Net.Http.Json;

namespace MySpot.Tests.Integration.Controllers
{
    public class UsersControllerTest : ControllerTest, IDisposable
    {
        private readonly TestDatabase _testDatabase;
        public UsersControllerTest(OptionsProvider optionsProvider) : base(optionsProvider)
        {
            _testDatabase = new TestDatabase();
        }

        [Fact]
        public async Task post_usrs_should_return_no_content_status_code()
        {
            await _testDatabase.Context.Database.MigrateAsync();

            var command = new SignUp(Guid.Empty, "test2-email@myspot.io",
                "test-user2", "P@ssword1!", "John Doe", "user");

            var response = await Client.PostAsJsonAsync("users", command);
            response.StatusCode.ShouldBe(HttpStatusCode.Created);
        }

        [Fact]
        public async Task post_sing_in_should_return_ok_200_status_code_and_jwt_token()
        {
            // Arrange
            var passwordManager = new PasswordManager(new PasswordHasher<User>());
            var clock = new Clock();
            const string password = "secret";

            var user = new User(Guid.NewGuid(), "test-user-001@myspot.io", "test-user-001",
                passwordManager.Secure(password), "Test Doe", Role.User(), clock.Current());

            await _testDatabase.Context.Database.MigrateAsync();
            await _testDatabase.Context.Users.AddAsync(user);
            await _testDatabase.Context.SaveChangesAsync();

            // Act
            var command = new SignIn(user.Email, password);
            var response = await Client.PostAsJsonAsync("users/sign-in", command);
            var jwt = await response.Content.ReadFromJsonAsync<JwtDto>();

            // Assert
            jwt.ShouldNotBeNull();
            jwt.AccessToken.ShouldNotBeNullOrWhiteSpace();
        }

        [Fact]
        public async Task get_users_me_should_return_ok_200_status_code_and_user()
        {
            // Arrange
            var passwordManager = new PasswordManager(new PasswordHasher<User>());
            var clock = new Clock();
            const string password = "secret";

            var user = new User(Guid.NewGuid(), "test-user-001@myspot.io", "test-user-001",
                passwordManager.Secure(password), "Test Doe", Role.User(), clock.Current());

            await _testDatabase.Context.Database.MigrateAsync();
            await _testDatabase.Context.Users.AddAsync(user);
            await _testDatabase.Context.SaveChangesAsync();

            // Act
            Authorize(user.Id, user.Role);
            var userDto = await Client.GetFromJsonAsync<UserDto>("users/me");

            // Assert
            userDto.ShouldNotBeNull();
            userDto.Id.ShouldBe(user.Id.Value);
        }

        public void Dispose()
        {
            _testDatabase.Dispose();
        }
    }
}
