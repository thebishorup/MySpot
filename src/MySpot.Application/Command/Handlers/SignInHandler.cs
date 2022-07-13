using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Application.Security;
using MySpot.Core.Abstractions;
using MySpot.Core.Repositories;

namespace MySpot.Application.Command.Handlers
{
    public sealed class SignInHandler : ICommandHandler<SignIn>
    {
        private readonly IPasswordManager _passwordManager;
        private readonly IUserRepository _userRepository;

        public SignInHandler(IPasswordManager passwordManager, 
            IUserRepository userRepository)
        {
            _passwordManager = passwordManager;
            _userRepository = userRepository;
        }

        public async Task HandleAsync(SignIn command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user is null)
                throw new InvalidCredentialsException();

            if (!_passwordManager.Validate(command.Password, user.Password))
                throw new InvalidCredentialsException();
        }
    }
}
