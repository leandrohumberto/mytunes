using MediatR;

namespace MyTunes.Application.Queries.UserExists
{
    public class UserExistsQuery : IRequest<bool>
    {
        public UserExistsQuery(int id) => Id = id;

        public int Id { get; private set; }
    }
}
