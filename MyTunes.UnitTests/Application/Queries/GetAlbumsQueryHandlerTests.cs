using AutoMapper;
using Moq;
using MyTunes.Application.Queries.GetAlbums;
using MyTunes.Application.ViewModels.Album;
using MyTunes.Application.ViewModels.Track;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Queries
{
    public class GetAlbumsQueryHandlerTests
    {
        [Theory]
        [InlineData(null, null, null, null, null)]
        [InlineData("artist", "title", 1999U, "genre", AlbumFormat.FullLength)]
        public async void AnyAlbumFound_Executed_ReturnFoundAlbums(string? artist, string? title, uint? year, string? genre, AlbumFormat? format)
        {
            var albumRepositoryMock = new Mock<IAlbumRepository>();
            var mapperMock = new Mock<IMapper>();
            var query = new GetAlbumsQuery();
            var handler = new GetAlbumsQueryHandler(albumRepositoryMock.Object, mapperMock.Object);

            var album = new Album("name", 1, 1U, "genre", It.IsAny<AlbumFormat>());
            var albums = Enumerable.Empty<Album>().Append(album);
            var albumViewModel = new AlbumViewModel(It.IsAny<int>(), "name", "artist", 1U, "genre",
                It.IsAny<AlbumFormat>(), It.IsAny<IEnumerable<TrackViewModel>>());

            query.Artist = artist;
            query.Name = title;
            query.Year = year;
            query.Genre = genre;
            query.Format = format;

            albumRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<uint?>(), It.IsAny<string?>(), It.IsAny<AlbumFormat?>(), CancellationToken.None).Result)
                .Returns(albums);
            mapperMock.Setup(m => m.Map<AlbumViewModel>(It.IsAny<Album>())).Returns(albumViewModel);

            // Act
            var returnedAlbums = await handler.Handle(query);

            // Assert
            Assert.Contains(albumViewModel, returnedAlbums);
            albumRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<uint?>(), It.IsAny<string?>(), It.IsAny<AlbumFormat?>(), CancellationToken.None).Result,
                Times.Once);
            mapperMock.Verify(m => m.Map<AlbumViewModel>(It.IsAny<Album>()), Times.AtLeastOnce);
        }

        [Theory]
        [InlineData(null, null, null, null, null)]
        [InlineData("artist", "title", 1999U, "genre", AlbumFormat.FullLength)]
        public async void NoAlbumsFound_Executed_ReturnNoAlbums(string? artist, string? title, uint? year, string? genre, AlbumFormat? format)
        {
            // Arrange
            var albumRepositoryMock = new Mock<IAlbumRepository>();
            var mapperMock = new Mock<IMapper>();
            var query = new GetAlbumsQuery();
            var handler = new GetAlbumsQueryHandler(albumRepositoryMock.Object, mapperMock.Object);

            query.Artist = artist;
            query.Name = title;
            query.Year = year;
            query.Genre = genre;
            query.Format = format;

            albumRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<uint?>(), It.IsAny<string?>(), It.IsAny<AlbumFormat?>(), CancellationToken.None).Result)
                .Returns(Enumerable.Empty<Album>());
            mapperMock.Setup(m => m.Map<AlbumViewModel>(It.IsAny<Album>()));

            // Act
            var returnedAlbums = await handler.Handle(query);

            // Assert
            Assert.Empty(returnedAlbums);
            albumRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<string?>(), It.IsAny<string?>(),
                It.IsAny<uint?>(), It.IsAny<string?>(), It.IsAny<AlbumFormat?>(), CancellationToken.None).Result,
                Times.Once);
            mapperMock.Verify(m => m.Map<AlbumViewModel>(It.IsAny<Album>()), Times.Never);
        }
    }
}
