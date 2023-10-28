using Moq;
using MyTunes.Application.Commands.DeleteAlbum;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Commands
{
    public class DeleteAlbumCommandHandlerTests
    {
        [Fact]
        public async void AlbumExists_Executed_DeleteAlbum()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IAlbumRepository>();
            var command = new DeleteAlbumCommand(It.IsAny<int>());
            var handler = new DeleteAlbumCommandHandler(albumRepositoryMock.Object);

            _ = albumRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(true);
            _ = albumRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>(), CancellationToken.None));

            // Act
            var unit = await handler.Handle(command);

            // Assert
            albumRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            albumRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void AlbumDoesNotExist_Executed_DoNotDeleteAlbum()
        {
            // Arrange
            var albumRepositoryMock = new Mock<IAlbumRepository>();
            var command = new DeleteAlbumCommand(It.IsAny<int>());
            var handler = new DeleteAlbumCommandHandler(albumRepositoryMock.Object);

            _ = albumRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(false);
            _ = albumRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>(), CancellationToken.None));

            // Act
            var unit = await handler.Handle(command);

            // Assert
            albumRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            albumRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
        }
    }
}
