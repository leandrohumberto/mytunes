using MyTunes.Application.InputModels.Artist;
using MyTunes.Application.ViewModels.Artist;

namespace MyTunes.Application.Services.Interfaces
{
    public interface IArtistService
    {
        Task<IEnumerable<ArtistViewModel>> Get(GetArtistsInputModel? inputModel);
        Task<ArtistViewModel> GetById(int id);
        Task<int> Create(CreateArtistInputModel inputModel, CancellationToken cancellationToken = default);
        Task Update(int id, UpdateArtistInputModel inputModel, CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
    }
}
