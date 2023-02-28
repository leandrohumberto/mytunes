using MyTunes.Application.InputModels.Artist;
using MyTunes.Application.Services.Interfaces;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Core.Entities;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Services.Implementations
{
    public class ArtistService : IArtistService
    {
        private readonly MyTunesDbContext _dbContext;

        public ArtistService(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int Create(CreateArtistInputModel inputModel)
        {
            var artist = new Artist(inputModel.Name, inputModel.Biography);

            var id = _dbContext.Artists.Keys.Any() ? _dbContext.Artists.Keys.Max() + 1 : 1;
            _dbContext.Artists.Add(id, artist);
            return artist.Id;
        }

        public void Delete(int id)
        {
            if (_dbContext.Artists.Any(p => p.Key == id))
            {
                _dbContext.Artists.Remove(id);
            }
        }

        public IEnumerable<ArtistViewModel> Get(GetArtistsInputModel? inputModel)
        {
            if (inputModel == null || (string.IsNullOrWhiteSpace(inputModel?.Name)))
            {
                return _dbContext.Artists.Select(p => new ArtistViewModel(p.Key, p.Value.Name, p.Value.Biography));
            }

            return _dbContext.Artists
                .Where(p => p.Value.Name == inputModel.Name.Trim())
                .Select(p => new ArtistViewModel(p.Key, p.Value.Name, p.Value.Biography));
        }

        public ArtistViewModel GetById(int id)
        {
            var artist = _dbContext.Artists.Where(p => p.Key == id).Single();

            if (artist.Value != null)
            {
                return new ArtistViewModel(artist.Key, artist.Value.Name, artist.Value.Biography);
            }

            throw new Exception($"No artist found for the given Id ({id}).");
        }

        public void Update(int id, UpdateArtistInputModel inputModel)
        {
            var artist = _dbContext.Artists.Where(p => p.Key == id).Single().Value;

            artist?.Update(inputModel.Name, inputModel.Biography);
        }
    }
}
