using MediatR;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class AlbumsController : ControllerBase
    {
        private const string applicationJsonMediaType = "application/json";
        private readonly IMediator _mediator;

        public AlbumsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/albums GET
        [HttpGet(Name = "GetAlbums")]
        [Authorize(Roles = "Admin,Viewer")]
        [ProducesResponseType(typeof(IEnumerable<AlbumViewModel>), StatusCodes.Status200OK, applicationJsonMediaType)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Get([FromQuery] GetAlbumsQuery? inputModel)
        {
            var albums = inputModel != null ? await _mediator.Send(inputModel) : Enumerable.Empty<AlbumViewModel>();
            return Ok(albums);
        }

        // api/albums/{id} GET
        [HttpGet("{id:int}", Name = "GetAlbumById")]
        [Authorize(Roles = "Admin,Viewer")]
        [ProducesResponseType(typeof(AlbumViewModel), StatusCodes.Status200OK, applicationJsonMediaType)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(typeof(CreateAlbumCommand), StatusCodes.Status201Created, applicationJsonMediaType)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, applicationJsonMediaType)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post([FromBody] CreateAlbumCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        // api/albums/{id} PUT
        [HttpPut("{id:int}", Name = "UpdateAlbum")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest, applicationJsonMediaType)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
