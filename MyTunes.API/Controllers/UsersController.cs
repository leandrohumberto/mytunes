using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyTunes.Application.Commands.CreateUser;
using MyTunes.Application.Commands.LoginUser;
using MyTunes.Application.Queries.GetUserById;
using MyTunes.Application.Queries.UserExists;
using MyTunes.Application.ViewModels.User;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private const string applicationJsonMediaType = "application/json";
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/users/{id} GET
        [HttpGet("{id:int}", Name = "GetUserById")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK, applicationJsonMediaType)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            if (await _mediator.Send(new UserExistsQuery(id)))
            {
                return Ok(await _mediator.Send(new GetUserByIdQuery(id)));
            }

            return NotFound();
        }

        // api/users POST
        [HttpPost(Name = "CreateUser")]
        [AllowAnonymous]
        [Authorize(Roles = "Admin,Viewer")]
        [ProducesResponseType(typeof(CreateUserCommand), StatusCodes.Status201Created, applicationJsonMediaType)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, applicationJsonMediaType)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        // api/users/login PUT
        [HttpPut("login", Name = "LoginUser")]
        [AllowAnonymous]
        [Authorize(Roles = "Admin,Viewer")]
        [ProducesResponseType(typeof(LoginUserViewModel), StatusCodes.Status200OK, applicationJsonMediaType)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, applicationJsonMediaType)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var viewModel = await _mediator.Send(command);

            if (viewModel == null)
            {
                return BadRequest();
            }

            return Ok(viewModel);
        }
    }
}
