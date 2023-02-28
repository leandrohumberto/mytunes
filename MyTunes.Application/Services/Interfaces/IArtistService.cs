using MyTunes.Application.InputModels.Artist;
using MyTunes.Application.ViewModels.Artist;

namespace MyTunes.Application.Services.Interfaces
{
    public interface IArtistService
    {
        IEnumerable<ArtistViewModel> Get(GetArtistsInputModel? inputModel);
        ArtistViewModel GetById(int id);
        int Create(CreateArtistInputModel inputModel);
        void Update(int id, UpdateArtistInputModel inputModel);
        void Delete(int id);
    }
}
