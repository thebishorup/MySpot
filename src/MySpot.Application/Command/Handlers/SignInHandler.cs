using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Application.Security;
using MySpot.Core.Repositories;

namespace MySpot.Application.Command.Handlers
{
    public sealed class SignInHandler : ICommandHandler<SignIn>
    {
        private readonly IPasswordManager _passwordManager;
        private readonly IUserRepository _userRepository;
        private readonly IAuthenticator _authenticator;
        private readonly ITokenStorage _tokenStorage;

        public SignInHandler(IPasswordManager passwordManager, 
            IUserRepository userRepository, IAuthenticator authenticator,
            ITokenStorage tokenStorage)
        {
            _passwordManager = passwordManager;
            _userRepository = userRepository;
            _authenticator = authenticator;
            _tokenStorage = tokenStorage;
        }

        public async Task HandleAsync(SignIn command)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user is null)
                throw new InvalidCredentialsException();

            if (!_passwordManager.Validate(command.Password, user.Password))
                throw new InvalidCredentialsException();

            var jwt = _authenticator.CreateToken(user.Id, user.Role);

            _tokenStorage.Set(jwt);
        }
    }
}
