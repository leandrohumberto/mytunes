using AutoMapper;
using Moq;
using MyTunes.Application.Commands.UpdateAlbum;
using MyTunes.Application.InputModels.Track;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Commands
{
    public class UpdateAlbumCommandHandlerTests
    {
        [Fact]
        public async void AlbumExists_Executed_UpdateAlbum()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IAlbumRepository>();
            var mapperMock = new Mock<IMapper>();
            var command = new UpdateAlbumCommand(
                    name: "name",
                    year: 1U,
                    genre: "genre",
                    format: It.IsAny<AlbumFormat>(),
                    tracklist: Enumerable.Empty<TrackInputModel>().Append(new TrackInputModel(1, "track 01", TimeSpan.FromSeconds(10))));
            command.SetId(It.IsAny<int>());

            _ = albumRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(true);

            _ = albumRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(new Album("name", 1, 1U, "genre", It.IsAny<AlbumFormat>()));

            _ = albumRepositoryMock.Setup(r => r.SaveChangesAsync(It.IsAny<Album>(), CancellationToken.None));

            _ = mapperMock.Setup(m => m.Map<Track>(It.IsAny<TrackInputModel>()))
                .Returns(new Track(1, "track 01", TimeSpan.FromSeconds(10)));

            var handler = new UpdateAlbumCommandHandler(albumRepositoryMock.Object, mapperMock.Object);
            
            // Act
            await handler.Handle(command);

            // Assert
            albumRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            albumRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            albumRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<Album>(), CancellationToken.None),
                Times.Once);
            mapperMock.Verify(m => m.Map<Track>(It.IsAny<TrackInputModel>()), Times.AtLeastOnce);
        }

        [Fact]
        public async void AlbumDoesNotExist_Executed_DoNotUpdateAlbum()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IAlbumRepository>();
            var mapperMock = new Mock<IMapper>();
            var command = new UpdateAlbumCommand(
                    name: It.IsAny<string>(),
                    year: It.IsAny<uint>(),
                    genre: It.IsAny<string>(),
                    format: It.IsAny<AlbumFormat>(),
                    tracklist: It.IsAny<IEnumerable<TrackInputModel>>());
            command.SetId(It.IsAny<int>());

            _ = albumRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(false);

            var handler = new UpdateAlbumCommandHandler(albumRepositoryMock.Object, mapperMock.Object);

            // Act
            await handler.Handle(command);

            // Assert
            albumRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            albumRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Never);
            albumRepositoryMock.Verify(r => r.SaveChangesAsync(It.IsAny<Album>(), CancellationToken.None),
                Times.Never);
            mapperMock.Verify(m => m.Map<Track>(It.IsAny<TrackInputModel>()), Times.Never);
        }
    }
}
