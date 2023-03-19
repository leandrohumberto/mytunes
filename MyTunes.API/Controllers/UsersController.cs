using MediatR;
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
    public class UsersController : ControllerBase
    {
        private const string applicationJsonMediaType = "application/json";
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/users/{id} GET
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(UserViewModel), StatusCodes.Status200OK, applicationJsonMediaType)]
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
        [HttpPost]
        [ProducesResponseType(typeof(CreateUserCommand), StatusCodes.Status201Created, applicationJsonMediaType)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, applicationJsonMediaType)]
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        // api/users/login PUT
        [HttpPut("login")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK, applicationJsonMediaType)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest, applicationJsonMediaType)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, applicationJsonMediaType)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            if (await _mediator.Send(command))
            {
                return Ok("Logged in");
            }

            return BadRequest("Invalid Email or Password");
        }
    }
}
