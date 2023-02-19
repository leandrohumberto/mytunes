using Microsoft.AspNetCore.Mvc;
using MyTunes.API.DTOs;
using MyTunes.API.Models;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlbumsController : ControllerBase
    {
        private readonly List<Album> _albums = new()
        {
            new Album(1, "Impera", 2022,
                Enumerable.Empty<Track>()
                    .Append(new Track(1, 1, "TRACK 01"))
                    .Append(new Track(2, 2, "TRACK 02"))
                    .Append(new Track(3, 3, "TRACK 03"))
                    .Append(new Track(4, 4, "TRACK 04"))
                    .Append(new Track(5, 5, "TRACK 05"))
                    .Append(new Track(6, 6, "TRACK 06"))
                    .Append(new Track(7, 7, "TRACK 07"))
                    .Append(new Track(8, 8, "TRACK 08"))
                    .Append(new Track(9, 9, "TRACK 09"))
                    .Append(new Track(10, 10, "TRACK 10"))),
            new Album(2, "Leviathan", 2004,
                Enumerable.Empty<Track>()
                    .Append(new Track(1, 1, "TRACK 01"))
                    .Append(new Track(2, 2, "TRACK 02"))
                    .Append(new Track(3, 3, "TRACK 03"))
                    .Append(new Track(4, 4, "TRACK 04"))
                    .Append(new Track(5, 5, "TRACK 05"))
                    .Append(new Track(6, 6, "TRACK 06"))
                    .Append(new Track(7, 7, "TRACK 07"))
                    .Append(new Track(8, 8, "TRACK 08"))
                    .Append(new Track(9, 9, "TRACK 09"))
                    .Append(new Track(10, 10, "TRACK 10"))),
            new Album(3, "Metallica", 1991,
                Enumerable.Empty<Track>()
                    .Append(new Track(1, 1, "TRACK 01"))
                    .Append(new Track(2, 2, "TRACK 02"))
                    .Append(new Track(3, 3, "TRACK 03"))
                    .Append(new Track(4, 4, "TRACK 04"))
                    .Append(new Track(5, 5, "TRACK 05"))
                    .Append(new Track(6, 6, "TRACK 06"))
                    .Append(new Track(7, 7, "TRACK 07"))
                    .Append(new Track(8, 8, "TRACK 08"))
                    .Append(new Track(9, 9, "TRACK 09"))
                    .Append(new Track(10, 10, "TRACK 10"))),
            new Album(4, "Ascendancy", 2005,
                Enumerable.Empty<Track>()
                    .Append(new Track(1, 1, "TRACK 01"))
                    .Append(new Track(2, 2, "TRACK 02"))
                    .Append(new Track(3, 3, "TRACK 03"))
                    .Append(new Track(4, 4, "TRACK 04"))
                    .Append(new Track(5, 5, "TRACK 05"))
                    .Append(new Track(6, 6, "TRACK 06"))
                    .Append(new Track(7, 7, "TRACK 07"))
                    .Append(new Track(8, 8, "TRACK 08"))
                    .Append(new Track(9, 9, "TRACK 09"))
                    .Append(new Track(10, 10, "TRACK 10"))),
            new Album(5, "The Blackening", 2007,
                Enumerable.Empty<Track>()
                    .Append(new Track(1, 1, "TRACK 01"))
                    .Append(new Track(2, 2, "TRACK 02"))
                    .Append(new Track(3, 3, "TRACK 03"))
                    .Append(new Track(4, 4, "TRACK 04"))
                    .Append(new Track(5, 5, "TRACK 05"))
                    .Append(new Track(6, 6, "TRACK 06"))
                    .Append(new Track(7, 7, "TRACK 07"))
                    .Append(new Track(8, 8, "TRACK 08"))
                    .Append(new Track(9, 9, "TRACK 09"))
                    .Append(new Track(10, 10, "TRACK 10"))),
        };

        // api/albums GET
        [HttpGet(Name = "GetAlbums")]
        public IActionResult Get([FromQuery] GetAlbumsDto dto)
        {
            var result = _albums as IEnumerable<Album>;

            if (!string.IsNullOrWhiteSpace(dto?.Name))
            {
                result = result.Where(p => p.Name == dto.Name);
            }

            if (dto?.Year > 0)
            {
                result = result.Where(p => p.Year == dto.Year);
            }

            return Ok(result);
        }

        // api/albums/{id} GET
        [HttpGet("{id}", Name = "GetAlbumById")]
        public IActionResult GetById(int id)
        {
            if (_albums.Any(p => p.Id == id))
            {
                return Ok(_albums.Where(p => p.Id == id));
            }

            return NotFound();
        }

        // api/albums POST
        [HttpPost(Name = "CreateAlbmum")]
        public IActionResult Post([FromBody] CreateAlbumDto dto)
        {
            var album = new Album(_albums.Max(p => p.Id) + 1, dto.Name, dto.Year, dto.Tracklist);
            _albums.Add(album);

            return CreatedAtAction(nameof(GetById), new { album.Id }, album);
        }

        // api/albums/{id} PUT
        [HttpPut("{id}", Name = "UpdateAlbum")]
        public IActionResult Put(int id, [FromBody] UpdateAlbumDto dto)
        {
            var artist = _albums.Where(p => p.Id == id).SingleOrDefault();

            if (artist != null)
            {
                artist.Update(dto.Name, dto.Year, dto.Tracklist);
                return NoContent();
            }

            return NotFound();
        }

        // api/albums/{id} DELETE
        [HttpDelete("{id}", Name = "DeleteAlbum")]
        public IActionResult Delete(int id)
        {
            if (_albums.Any(p => p.Id == id))
            {
                _albums.Remove(_albums.Single(p => p.Id == id));
                return NoContent();
            }

            return NotFound();
        }
    }
}
