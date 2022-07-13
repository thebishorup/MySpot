using MySpot.Application.Abstractions;
using MySpot.Application.Exceptions;
using MySpot.Application.Security;
using MySpot.Core.Abstractions;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Command.Handlers
{
    public sealed class SignUpHandler : ICommandHandler<SignUp>
    {
        private readonly IClock _clock;
        private readonly IPasswordManager _passwordManager;
        private readonly IUserRepository _userRepository;

        public SignUpHandler(IClock clock, IPasswordManager passwordManager, IUserRepository userRepository)
        {
            _clock = clock;
            _passwordManager = passwordManager;
            _userRepository = userRepository;
        }

        public async Task HandleAsync(SignUp command)
        {
            var userId = new UserId(command.UserId);
            var email = new Email(command.Email);
            var userName = new UserName(command.UserName);
            var password = new Password(command.Password);
            var fullName = new FullName(command.FullName);
            var role = string.IsNullOrEmpty(command.Role) ? Role.User() : new Role(command.Role);

            if (await _userRepository.GetByEmailAsync(email) is not null)
                throw new EmailAlreadyInUserException(email);

            if(await _userRepository.GetByUserNameAsync(userName) is not null)
                throw new UserNameAlreadyInUserException(userName);

            var securePasseord = _passwordManager.Secure(password);
            var user = new User(userId, email, userName, securePasseord, 
                fullName, role, _clock.Current());

            await _userRepository.AddAsync(user);
        }
    }
}
