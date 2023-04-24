using AutoMapper;
using MyTunes.Application.Commands.CreateArtist;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Core.Entities;

namespace MyTunes.Application.Mappers
{
    public class ArtistProfile : Profile
    {
        public ArtistProfile()
        {
            CreateMap<Artist, ArtistViewModel>();
            
            CreateMap<CreateArtistCommand, Artist>()
                .ForMember(a => a.Biography,
                    opt => opt.MapFrom(viewModel => viewModel.Biography ?? string.Empty));
        }
    }
}
