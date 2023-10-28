using AutoMapper;
using Moq;
using MyTunes.Application.Commands;
using MyTunes.Application.InputModels.Track;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.Core.Exceptions;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Commands
{
    public class CreateAlbumCommandHandlerTests
    {
        [Fact]
        public async void ArtistExists_Executed_ReturnAlbumId()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IAlbumRepository>();
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateAlbumCommand(
                    name: It.IsAny<string>(),
                    idArtist: It.IsAny<int>(),
                    year: It.IsAny<uint>(),
                    genre: It.IsAny<string>(),
                    format: It.IsAny<AlbumFormat>(),
                    tracklist: Enumerable.Empty<TrackInputModel>());

            _ = artistRepositoryMock.Setup(a => a.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(true);
            _ = albumRepositoryMock.Setup(a => a.AddAsync(It.IsAny<Album>(), CancellationToken.None).Result)
                .Returns(It.IsAny<int>);

            var handler = new CreateAlbumCommandHandler(albumRepositoryMock.Object,
                artistRepositoryMock.Object, mapperMock.Object);

            // Act
            var id = await handler.Handle(command);

            // Assert
            Assert.True(id >= 0);
            artistRepositoryMock.Verify(a => a.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            albumRepositoryMock.Verify(a => a.AddAsync(It.IsAny<Album>(), CancellationToken.None).Result,
                Times.Once);
            mapperMock.Verify(m => m.Map<Album>(command), Times.Once);
        }

        [Fact]
        public async void ArtistDoesNotExist_Executed_ThrowException()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IAlbumRepository>();
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var mapperMock = new Mock<IMapper>();

            var command = new CreateAlbumCommand(
                    name: It.IsAny<string>(),
                    idArtist: It.IsAny<int>(),
                    year: It.IsAny<uint>(),
                    genre: It.IsAny<string>(),
                    format: It.IsAny<AlbumFormat>(),
                    tracklist: Enumerable.Empty<TrackInputModel>());

            _ = artistRepositoryMock.Setup(a => a.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(false);
            _ = albumRepositoryMock.Setup(a => a.AddAsync(It.IsAny<Album>(), CancellationToken.None).Result)
                .Returns(It.IsAny<int>);

            var handler = new CreateAlbumCommandHandler(albumRepositoryMock.Object,
                artistRepositoryMock.Object, mapperMock.Object);

            // Act

            // Assert
            _ = await Assert.ThrowsAnyAsync<ArtistNotFoundException>(() => handler.Handle(command, CancellationToken.None));
            artistRepositoryMock.Verify(a => a.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            albumRepositoryMock.Verify(a => a.AddAsync(It.IsAny<Album>(), CancellationToken.None).Result,
                Times.Never);
            mapperMock.Verify(m => m.Map<Album>(command), Times.Never);
        }
    }
}
