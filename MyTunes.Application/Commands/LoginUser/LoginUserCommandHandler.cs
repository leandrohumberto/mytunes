using MediatR;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, bool>
    {
        private readonly MyTunesDbContext _dbContext;

        public LoginUserCommandHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dbContext.Users.Any(p => p.Email == request.Email && p.Password == request.Password));
        }
    }
}
