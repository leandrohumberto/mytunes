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

        public async Task<int> Create(CreateArtistInputModel inputModel, CancellationToken cancellationToken = default)
        {
            var artist = new Artist(inputModel.Name, inputModel.Biography);
            _dbContext.Artists.Add(artist);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);
            return artist.Id;
        }

        public async Task Delete(int id, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Artists.Any(p => p.Id == id))
            {
                var album = _dbContext.Artists.Single(p => p.Id == id);
                _dbContext.Artists.Remove(album);
                _ = await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<IEnumerable<ArtistViewModel>> Get(GetArtistsInputModel? inputModel)
        {
            IEnumerable<ArtistViewModel> result;

            if (inputModel == null || string.IsNullOrWhiteSpace(inputModel?.Name))
            {
                result = _dbContext.Artists.Select(p => new ArtistViewModel(p.Id, p.Name, p.Biography));
            }
            else
            {
                result = _dbContext.Artists
                    .Where(p => p.Name == inputModel.Name.Trim())
                    .Select(p => new ArtistViewModel(p.Id, p.Name, p.Biography));
            }

            return await Task.FromResult(result);
        }

        public async Task<ArtistViewModel> GetById(int id)
        {
            if (_dbContext.Artists.Any(p => p.Id == id))
            {
                var artist = _dbContext.Artists.Where(p => p.Id == id).Single();
                return await Task.FromResult(new ArtistViewModel(artist.Id, artist.Name, artist.Biography));
            }

            throw new Exception($"No artist found for the given Id ({id}).");
        }

        public async Task Update(int id, UpdateArtistInputModel inputModel, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Artists.Any(p => p.Id == id))
            {
                var artist = _dbContext.Artists.Where(p => p.Id == id).Single();
                artist.Update(inputModel.Name, inputModel.Biography);
                _ = await _dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
