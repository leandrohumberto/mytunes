using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyTunes.Application.Commands.CreateArtist;
using MyTunes.Application.Commands.DeleteArtist;
using MyTunes.Application.Commands.UpdateArtist;
using MyTunes.Application.Queries.GetArtistById;
using MyTunes.Application.Queries.GetArtists;
using MyTunes.Application.ViewModels.Artist;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ArtistsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // api/artists?name=ABC GET
        [HttpGet(Name = "GetArtists")]
        public async Task<IActionResult> Get([FromQuery] GetArtistsQuery? query)
        {
            IEnumerable<ArtistViewModel> artists;

            if (query != null)
            {
                artists = await _mediator.Send(query);
            }
            else
            {
                artists = Enumerable.Empty<ArtistViewModel>();
            }

            return Ok(artists);
        }

        // api/artists/{id} GET
        [HttpGet("{id}", Name = "GetArtistById")]
        public async Task<IActionResult> GetById(int id)
        {
            // TODO: validar se existe Artist com o id informado e retornar NotFound caso contrário
            
            var viewModel = await _mediator.Send(new GetArtistByIdQuery(id));
            return Ok(viewModel);
        }

        // api/artists POST
        [HttpPost(Name = "CreateArtist")]
        public async Task<IActionResult> Post([FromBody] CreateArtistCommand command)
        {
            var id = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetById), new { id }, command);
        }

        // api/artists/{id} PUT
        [HttpPut("{id}", Name = "UpdateArtist")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateArtistCommand command)
        {
            // TODO: validar se existe Artist com o id informado e retornar NotFound caso contrário

            command.SetId(id);
            _ = await _mediator.Send(command);
            return NoContent();
        }

        // api/artists/{id} DELETE
        [HttpDelete("{id}", Name = "DeleteArtist")]
        public async Task<IActionResult> Delete(int id)
        {
            // TODO: validar se existe Artist com o id informado e retornar NotFound caso contrário

            _ = await _mediator.Send(new DeleteArtistCommand(id));
            return NoContent();
        }
    }
}
