using MediatR;
using MyTunes.Application.ViewModels.User;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        private readonly IUserRepository _repository;

        public GetUserByIdQueryHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return new UserViewModel(user.Name, user.Email, user.Role);
        }
    }
}
