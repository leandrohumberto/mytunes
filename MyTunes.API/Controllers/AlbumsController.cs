using Microsoft.AspNetCore.Mvc;
using MyTunes.Application.InputModels.Album;
using MyTunes.Application.Services.Interfaces;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IAlbumService _albumService;

        public AlbumsController(IAlbumService albumService)
        {
            _albumService = albumService;
        }

        // api/albums GET
        [HttpGet(Name = "GetAlbums")]
        public IActionResult Get([FromQuery] GetAlbumsInputModel? inputModel)
        {
            return Ok(_albumService.Get(inputModel));
        }

        // api/albums/{id} GET
        [HttpGet("{id}", Name = "GetAlbumById")]
        public IActionResult GetById(int id)
        {
            var viewModel = _albumService.GetById(id);
            if (viewModel != null)
            {
                return Ok(viewModel);
            }

            return NotFound();
        }

        // api/albums POST
        [HttpPost(Name = "CreateAlbmum")]
        public IActionResult Post([FromBody] CreateAlbumInputModel inputModel)
        {
            var id = _albumService.Create(inputModel);

            return CreatedAtAction(nameof(GetById), new { id }, inputModel);
        }

        // api/albums/{id} PUT
        [HttpPut("{id}", Name = "UpdateAlbum")]
        public IActionResult Put(int id, [FromBody] UpdateAlbumInputModel inputModel)
        {
            if (_albumService.GetById(id) == null)
            {
                return NotFound();
            }

            _albumService.Update(id, inputModel);
            return NoContent();
        }

        // api/albums/{id} DELETE
        [HttpDelete("{id}", Name = "DeleteAlbum")]
        public IActionResult Delete(int id)
        {
            if (_albumService.GetById(id) == null)
            {
                return NotFound();
            }

            _albumService.Delete(id);
            return NoContent();
        }
    }
}
