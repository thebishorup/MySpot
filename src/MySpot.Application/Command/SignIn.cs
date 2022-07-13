using MySpot.Application.Abstractions;

namespace MySpot.Application.Command
{
    public record SignIn(string Email, string Password) : ICommand;
}
