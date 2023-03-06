using MediatR;
using MyTunes.Application.ViewModels.User;
using MyTunes.Infrastructure.Persistence;

namespace MyTunes.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        private readonly MyTunesDbContext _dbContext;

        public GetUserByIdQueryHandler(MyTunesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_dbContext.Users
                .Where(p => p.Id == request.Id)
                .Select(p => new UserViewModel(p.Name, p.Email, p.Role)).SingleOrDefault())
            ?? throw new Exception("User not found");
        }
    }
}
