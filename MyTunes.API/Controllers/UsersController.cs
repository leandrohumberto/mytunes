using Microsoft.AspNetCore.Mvc;
using MyTunes.API.DTOs;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // api/users/{id} GET
        [HttpGet]
        public IActionResult GetById(int id)
        {
            return Ok(id);
        }

        // api/users POST
        [HttpPost]
        public IActionResult Post([FromBody] CreateUserDto dto)
        {
            return Ok(dto);
        }

        // api/users/{id}/login PUT
        [HttpPut("{id}/login")]
        public IActionResult Login([FromBody] LoginDto dto)
        {
            return Ok(dto);
        }
    }
}
