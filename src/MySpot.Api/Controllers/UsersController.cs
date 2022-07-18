using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using MySpot.Application.Abstractions;
using MySpot.Application.Command;
using MySpot.Application.DTO;
using MySpot.Application.Queries;
using MySpot.Application.Security;

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
        private readonly ITokenStorage _tokenStorage;

        public UsersController(IQueryHandler<GetUser, UserDto> getUserHandler, IQueryHandler<GetUsers, IEnumerable<UserDto>> getUsersHandler, ICommandHandler<SignUp> signUpHandler, 
            ICommandHandler<SignIn> signInHandler,
            ITokenStorage tokenStorage)
        {
            _getUserHandler = getUserHandler;
            _getUsersHandler = getUsersHandler;
            _signUpHandler = signUpHandler;
            _signInHandler = signInHandler;
            _tokenStorage = tokenStorage;
        }

        [HttpGet("{userId:Guid}")]
        [Authorize(Policy = "is-admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> Get(Guid userId)
        {
            var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
            if (user is null)
                return NotFound();

            return user;
        }

        [Authorize]
        [HttpGet("me")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<UserDto>> Get()
        {
            if (string.IsNullOrWhiteSpace(User.Identity?.Name)) return NotFound();

            var userId = Guid.Parse(User.Identity.Name);

            var user = await _getUserHandler.HandleAsync(new GetUser { UserId = userId });
            if (user is null)
                return NotFound();

            return user;
        }

        [HttpGet]
        [Authorize(Policy = "is-admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] GetUsers users)
            => Ok(await _getUsersHandler.HandleAsync(users));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Post(SignUp command)
        {
            command = command with { UserId = Guid.NewGuid() } as SignUp;
            await _signUpHandler.HandleAsync(command);
            return CreatedAtAction(nameof(Get), new {command.UserId}, null);
        }

        [HttpPost("sign-in")]
        public async Task<ActionResult<JwtDto>> Post(SignIn command)
        {
            await _signInHandler.HandleAsync(command);
            var jwt = _tokenStorage.Get();
            return jwt;
        }
    }
}
