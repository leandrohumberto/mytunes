using MyTunes.Application.InputModels.Album;
using MyTunes.Application.ViewModels.Album;

namespace MyTunes.Application.Services.Interfaces
{
    public interface IAlbumService
    {
        Task<IEnumerable<AlbumViewModel>> Get(GetAlbumsInputModel? inputModel);
        Task<AlbumViewModel> GetById(int id);
        Task<int> Create(CreateAlbumInputModel inputModel, CancellationToken cancellationToken = default);
        Task Update(int id, UpdateAlbumInputModel inputModel, CancellationToken cancellationToken = default);
        Task Delete(int id, CancellationToken cancellationToken = default);
    }
}
