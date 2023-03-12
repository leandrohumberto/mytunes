using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTunes.Application.Commands.CreateUser;
using MyTunes.Application.Commands.LoginUser;
using MyTunes.Application.Queries.GetUserById;
using MyTunes.Application.Queries.UserExists;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/users/{id} GET
        [HttpGet("{id:int}")]
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
        public async Task<IActionResult> Post([FromBody] CreateUserCommand command)
        {
            var id = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        // api/users/login PUT
        [HttpPut("login")]
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
