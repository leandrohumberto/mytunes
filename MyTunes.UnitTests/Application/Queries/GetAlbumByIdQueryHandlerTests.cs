using AutoMapper;
using Moq;
using MyTunes.Application.Queries.GetAlbumById;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Queries
{
    public class GetAlbumByIdQueryHandlerTests
    {
        [Fact]
        public async void AlbumExists_Executed_ReturnAlbum()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IAlbumRepository>();
            var mapperMock = new Mock<IMapper>();
            var query = new GetAlbumByIdQuery(It.IsAny<int>());
            var handler = new GetAlbumByIdQueryHandler(albumRepositoryMock.Object, mapperMock.Object);

            var tracklist = Enumerable.Empty<Track>().Append(new Track(1U, "name", TimeSpan.FromSeconds(10)));
            var album = new Album("name", 1, 1U, "genre", It.IsAny<AlbumFormat>(), tracklist);

            var tracklistViewModel = Enumerable.Empty<TrackViewModel>().Append(new TrackViewModel(1U, "name", TimeSpan.FromSeconds(10)));
            var albumViewModel = new AlbumViewModel(1, "name", "artist", 1U, "genre", It.IsAny<AlbumFormat>(), tracklistViewModel);
            
            albumRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(album);
            mapperMock.Setup(m => m.Map<AlbumViewModel>(It.IsAny<Album>())).Returns(albumViewModel);

            // Act
            var returnedViewModel = await handler.Handle(query);

            // Assert
            Assert.Equal(albumViewModel, returnedViewModel);
            albumRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            mapperMock.Verify(m => m.Map<AlbumViewModel>(It.IsAny<Album>()), Times.Once);
        }
    }
}
