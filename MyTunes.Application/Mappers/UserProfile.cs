using AutoMapper;
using MyTunes.Application.Commands.CreateUser;
using MyTunes.Application.ViewModels.User;
using MyTunes.Core.Entities;

namespace MyTunes.Application.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserViewModel>();
            
            CreateMap<CreateUserCommand, User>();
        }
    }
}
