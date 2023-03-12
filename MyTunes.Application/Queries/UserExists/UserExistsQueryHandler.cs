using MediatR;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.UserExists
{
    public class UserExistsQueryHandler : IRequestHandler<UserExistsQuery, bool>
    {
        private readonly IUserRepository _userRepository;

        public UserExistsQueryHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<bool> Handle(UserExistsQuery request, CancellationToken cancellationToken = default)
            => await _userRepository.ExistsAsync(request.Id, cancellationToken);
    }
}
