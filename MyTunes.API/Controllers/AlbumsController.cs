using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTunes.Application.Commands;
using MyTunes.Application.Commands.DeleteAlbum;
using MyTunes.Application.Commands.UpdateAlbum;
using MyTunes.Application.Queries.AlbumExists;
using MyTunes.Application.Queries.GetAlbumById;
using MyTunes.Application.Queries.GetAlbums;
using MyTunes.Application.ViewModels.Album;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AlbumsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/albums GET
        [HttpGet(Name = "GetAlbums")]
        public async Task<IActionResult> Get([FromQuery] GetAlbumsQuery? inputModel)
        {
            var albums = inputModel != null ? await _mediator.Send(inputModel) : Enumerable.Empty<AlbumViewModel>();
            return Ok(albums);
        }

        // api/albums/{id} GET
        [HttpGet("{id:int}", Name = "GetAlbumById")]
        public async Task<IActionResult> GetById(int id)
        {
            if (await _mediator.Send(new AlbumExistsQuery(id)))
            {
                var viewModel = await _mediator.Send(new GetAlbumByIdQuery(id));
                return Ok(viewModel);
            }

            return NotFound();
        }

        // api/albums POST
        [HttpPost(Name = "CreateAlbum")]
        public async Task<IActionResult> Post([FromBody] CreateAlbumCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        // api/albums/{id} PUT
        [HttpPut("{id:int}", Name = "UpdateAlbum")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateAlbumCommand command)
        {
            if (await _mediator.Send(new AlbumExistsQuery(id)))
            {
                command.SetId(id);
                await _mediator.Send(command);
                return NoContent();
            }

            return NotFound();
        }

        // api/albums/{id} DELETE
        [HttpDelete("{id:int}", Name = "DeleteAlbum")]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _mediator.Send(new AlbumExistsQuery(id)))
            {
                await _mediator.Send(new DeleteAlbumCommand(id));
                return NoContent();
            }

            return NotFound();
        }
    }
}
