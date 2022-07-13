using MySpot.Application.Abstractions;

namespace MySpot.Application.Command
{
    public record SignUp(Guid UserId, string Email, string UserName, string Password, string FullName, string Role) : ICommand;
}
