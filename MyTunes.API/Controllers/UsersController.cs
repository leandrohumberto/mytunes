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
        public IActionResult GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        // api/users POST
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserInputModel inputModel)
        {
            var id = _userService.Create(inputModel);
            return CreatedAtAction(nameof(GetById), new { id }, inputModel);
        }

        // api/users/{id}/login PUT
        [HttpPut("login")]
        public IActionResult Login([FromBody] LoginUserInputModel inputModel)
        {
            if (_userService.Login(inputModel))
            {
                return Ok("Logged in");
            }

            return BadRequest("Invalid Email or Password");
        }
    }
}
