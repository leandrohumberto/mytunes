using Microsoft.AspNetCore.Mvc;
using MyTunes.Application.InputModels.Artist;
using MyTunes.Application.Services.Interfaces;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        // api/artists?name=ABC GET
        [HttpGet(Name = "GetArtists")]
        public IActionResult Get([FromQuery] GetArtistsInputModel? inputModel)
        {
            return Ok(_artistService.Get(inputModel));
        }

        // api/artists/{id} GET
        [HttpGet("{id}", Name = "GetArtistById")]
        public IActionResult GetById(int id)
        {
            var viewModel = _artistService.GetById(id);
            if (viewModel != null)
            {
                return Ok(viewModel);
            }

            return NotFound();
        }

        // api/artists POST
        [HttpPost(Name = "CreateArtist")]
        public IActionResult Post([FromBody] CreateArtistInputModel inputModel)
        {
            var id = _artistService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id }, inputModel);
        }

        // api/artists/{id} PUT
        [HttpPut("{id}", Name = "UpdateArtist")]
        public IActionResult Put(int id, [FromBody] UpdateArtistInputModel inputModel)
        {
            if (_artistService.GetById(id) == null)
            {
                return NotFound();
            }

            _artistService.Update(id, inputModel);
            return NoContent();
        }

        // api/artists/{id} DELETE
        [HttpDelete("{id}", Name = "DeleteArtist")]
        public IActionResult Delete(int id)
        {
            if (_artistService.GetById(id) == null)
            {
                return NotFound();
            }

            _artistService.Delete(id);
            return NoContent();
        }
    }
}
