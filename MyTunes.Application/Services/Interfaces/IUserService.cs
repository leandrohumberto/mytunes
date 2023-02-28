using MyTunes.Application.InputModels.User;
using MyTunes.Application.ViewModels.User;

namespace MyTunes.Application.Services.Interfaces
{
    public interface IUserService
    {
        UserViewModel GetById(int id);
        int Create(CreateUserInputModel inputModel);
        bool Login(LoginUserInputModel inputModel);
    }
}
