using Microsoft.AspNetCore.Mvc;

using MySpot.Application.Abstractions;
using MySpot.Application.Command;
using MySpot.Application.DTO;
using MySpot.Application.Queries;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IQueryHandler<GetUser, UserDto> _getUserHandler;
        private readonly IQueryHandler<GetUsers, IEnumerable<UserDto>> _getUsersHandler;
        private readonly ICommandHandler<SignUp> _signUpHandler;
        private readonly ICommandHandler<SignIn> _signInHandler;

        public UsersController(IQueryHandler<GetUser, UserDto> getUserHandler, IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler, ICommandHandler<SignUp> signUpHandler, 
            ICommandHandler<SignIn> signInHandler)
        {
            _getUserHandler = getUserHandler;
            _getUsersHandler = getUsersHandler;
            _signUpHandler = signUpHandler;
            _signInHandler = signInHandler;
        }

        [HttpGet("{userId: Guid}")]
        public async Task<ActionResult<UserDto>> Get(Guid userId)
        {
            var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
            if (user is null)
                return NotFound();

            return user;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers users)
            => Ok(await _getUsersHandler.HandleAsync(users));

        [HttpPost]
        public async Task<ActionResult> Post(SignUp command)
        {
            command = command with { UserId = Guid.NewGuid() } as SignUp;
            await _signUpHandler.HandleAsync(command);
            return CreatedAtAction(nameof(Get), new {command.UserId}, null);
        }
    }
}
