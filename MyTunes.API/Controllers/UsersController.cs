using Microsoft.AspNetCore.Mvc;
using MyTunes.Application.InputModels.User;
using MyTunes.Application.Services.Interfaces;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // api/users/{id} GET
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userService.GetById(id));
        }

        // api/users POST
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUserInputModel inputModel)
        {
            var id = await _userService.Create(inputModel);
            return CreatedAtAction(nameof(GetById), new { id }, inputModel);
        }

        // api/users/{id}/login PUT
        [HttpPut("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserInputModel inputModel)
        {
            if (await _userService.Login(inputModel))
            {
                return Ok("Logged in");
            }

            return BadRequest("Invalid Email or Password");
        }
    }
}
