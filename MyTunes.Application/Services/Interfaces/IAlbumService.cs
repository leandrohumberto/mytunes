using MyTunes.Application.InputModels.Album;
using MyTunes.Application.ViewModels.Album;

namespace MyTunes.Application.Services.Interfaces
{
    public interface IAlbumService
    {
        IEnumerable<AlbumViewModel> Get(GetAlbumsInputModel? inputModel);
        AlbumViewModel GetById(int id);
        int Create(CreateAlbumInputModel inputModel);
        void Update(int id, UpdateAlbumInputModel inputModel);
        void Delete(int id);
    }
}
