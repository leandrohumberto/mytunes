using AutoMapper;
using MyTunes.Application.InputModels.Track;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Entities;

namespace MyTunes.Application.Mappers
{
    public class TrackProfile : Profile
    {
        public TrackProfile()
        {
            CreateMap<TrackInputModel, Track>();
            
            CreateMap<Track, TrackViewModel>();
        }
    }
}
