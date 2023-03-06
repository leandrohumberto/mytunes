using MediatR;
using Microsoft.EntityFrameworkCore;
using MyTunes.Core.Entities;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
    {
        private readonly MyTunesDbContext _dbContext;

        public CreateUserCommandHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User(request.Name, request.Email, request.Password, request.Role);
            _dbContext.Users.Add(user);
            _ = await _dbContext.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
