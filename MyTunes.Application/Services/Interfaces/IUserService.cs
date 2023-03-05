using MyTunes.Application.InputModels.User;
using MyTunes.Application.ViewModels.User;

namespace MyTunes.Application.Services.Interfaces
{
    public interface IUserService
    {
        Task<UserViewModel> GetById(int id);
        Task<int> Create(CreateUserInputModel inputModel, CancellationToken cancellationToken = default);
        Task<bool> Login(LoginUserInputModel inputModel);
    }
}
