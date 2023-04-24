using AutoMapper;
using MediatR;
using MyTunes.Application.ViewModels.User;
using MyTunes.Core.Repositories;

namespace MyTunes.Application.Queries.GetUserById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, UserViewModel>
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<UserViewModel> Handle(GetUserByIdQuery request, CancellationToken cancellationToken = default)
        {
            var user = await _repository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<UserViewModel>(user);
        }
    }
}
