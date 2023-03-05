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
        public async Task<IActionResult> Get([FromQuery] GetArtistsInputModel? inputModel)
        {
            return Ok(await _artistService.Get(inputModel));
        }

        // api/artists/{id} GET
        [HttpGet("{id}", Name = "GetArtistById")]
        public async Task<IActionResult> GetById(int id)
        {
            var viewModel = await _artistService.GetById(id);
            if (viewModel != null)
            {
                return Ok(viewModel);
            }

            return NotFound();
        }

        // api/artists POST
        [HttpPost(Name = "CreateArtist")]
        public async Task<IActionResult> Post([FromBody] CreateArtistInputModel inputModel)
        {
            var id = await _artistService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id }, inputModel);
        }

        // api/artists/{id} PUT
        [HttpPut("{id}", Name = "UpdateArtist")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateArtistInputModel inputModel)
        {
            if (await _artistService.GetById(id) == null)
            {
                return NotFound();
            }

            await _artistService.Update(id, inputModel);
            return NoContent();
        }

        // api/artists/{id} DELETE
        [HttpDelete("{id}", Name = "DeleteArtist")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _artistService.GetById(id) == null)
            {
                return NotFound();
            }

            await _artistService.Delete(id);
            return NoContent();
        }
    }
}
