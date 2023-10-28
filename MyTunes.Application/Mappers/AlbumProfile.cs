using AutoMapper;
using MyTunes.Application.Commands;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Entities;

namespace MyTunes.Application.Mappers
{
    public class AlbumProfile : Profile
    {
        public AlbumProfile()
        {
            CreateMap<CreateAlbumCommand, Album>()
                .ForMember(album => album.Tracklist,
                    opt => opt.MapFrom(command => command.Tracklist.Select(t => new Track(t.Number, t.Title, t.Length)).ToList()));

            CreateMap<Album, AlbumViewModel>()
                .ForMember(viewModel => viewModel.Tracklist,
                    opt => opt.MapFrom(album => album.Tracklist.Select(t => new TrackViewModel(t.Number, t.Title, t.Length)).ToList()));
        }
    }
}
