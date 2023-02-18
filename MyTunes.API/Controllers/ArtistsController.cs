using Microsoft.AspNetCore.Mvc;
using MyTunes.API.DTOs;
using MyTunes.API.Models;

namespace MyTunes.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly List<Artist> _artists = new()
        {
            new Artist(1, "Ghost", "Melding surprisingly accessible metal- and pop-driven hard rock, costumed Swedish outfit Ghost deliver sonic sermons centered on horror imagery, the occult, and Satanic themes. The group is fronted by lead singer, songwriter, and conceptualist Tobias Forge, who dons various demonic Pope costumes and is backed by a band of \"Nameless Ghouls\" hidden in cloaks and heavy makeup."),
            new Artist(2, "Mastodon", "Atlanta's Mastodon are one of the most original and influential American metal bands to appear in the 21st century. Their wide-angle progressive approach encompasses stoner and sludge metal, punishing hardcore and metalcore, neo-psych, death metal, and more. The group's playing style incorporates technically complex guitar riffs, lyric hooks, long, melodic instrumental passages, and intricate, jazz-influenced drumming with syncopated time signatures."),
            new Artist(3, "Metallica", "Metallica is one of the most important and most influential metal bands of all time, drafting the blueprint for thrash metal in their earliest days, then pushing the boundaries of mainstream metal and hard rock as they settled into their role of a popular legacy act as the decades went on."),
            new Artist(4, "Trivium", "Orlando, Florida's Trivium are among the most provocative, restless, and influential bands to emerge from the American South's heavy metal explosion in the early 21st century. Beginning as a metalcore outfit, they have relentlessly combined and crossed styles including thrash, prog, technical, and melodic death metal, as well as alternative and groove metal."),
            new Artist(5, "Machine Head", "A hugely influential West Coast heavy metal quartet, Machine Head's neck-snapping riffs and earth-shaking grooves helped shape the New Wave of American Heavy Metal movement of the early mid-'90s. The band's aggressive and wide-ranging style, which combines elements of thrash, groove, and nu-metal, has yielded seminal contemporary metal efforts like Burn My Eyes (1994), The Burning Red (1999), Through the Ashes of Empires (2003), and The Blackening (2007)."),
        };

        // api/artists?name=ABC GET
        [HttpGet(Name = "GetArtists")]
        public IActionResult Get([FromQuery] GetArtistsDto? dto)
        {
            if (!string.IsNullOrWhiteSpace(dto?.Name))
            {
                return Ok(_artists.Where(p => p.Name?.Trim() == dto?.Name?.Trim()));
            }

            return Ok(_artists);
        }

        // api/artists/{id} GET
        [HttpGet("{id}", Name = "GetArtistsById")]
        public IActionResult GetById(int id)
        {
            if (_artists.Any(p => p.Id == id))
            {
                return Ok(_artists.Where(p => p.Id == id));
            }

            return NotFound();
        }

        // api/artists POST
        [HttpPost(Name = "CreateArtist")]
        public IActionResult Post([FromBody] CreateArtistDto dto)
        {
            var artist = new Artist(_artists.Max(p => p.Id) + 1, dto.Name, dto.Biography);
            _artists.Add(artist);

            return CreatedAtAction(nameof(GetById), new { artist.Id }, artist);
        }

        // api/artists/{id} PUT
        [HttpPut("{id}", Name = "UpdateArtist")]
        public IActionResult Put(int id, [FromBody] UpdateArtistDto dto)
        {
            var artist = _artists.Where(p => p.Id == id).SingleOrDefault();

            if (artist != null)
            {
                artist.Update(dto.Name, dto.Biography);
                return NoContent();
            }

            return NotFound();
        }

        // api/artists/{id} DELETE
        [HttpDelete("{id}", Name = "DeleteArtist")]
        public IActionResult Delete(int id)
        {
            if (_artists.Any(p => p.Id == id))
            {
                _artists.Remove(_artists.Single(p => p.Id == id));
                return NoContent();
            }

            return NotFound();
        }
    }
}
