using Moq;
using MyTunes.Application.Commands.DeleteArtist;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Commands
{
    public class DeleteArtistCommandHandlerTests
    {
        [Fact]
        public async void ArtistExists_Executed_DeleteArtist()
        {
            // Arrange
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var command = new DeleteArtistCommand(It.IsAny<int>());
            var handler = new DeleteArtistCommandHandler(artistRepositoryMock.Object);

            _ = artistRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(true);
            _ = artistRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>(), CancellationToken.None));

            // Act
            var unit = await handler.Handle(command);

            // Assert
            artistRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            artistRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void ArtistDoesNotExist_Executed_DoNotDeleteAlbum()
        {
            // Arrange
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var command = new DeleteArtistCommand(It.IsAny<int>());
            var handler = new DeleteArtistCommandHandler(artistRepositoryMock.Object);

            _ = artistRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(false);
            _ = artistRepositoryMock.Setup(r => r.DeleteAsync(It.IsAny<int>(), CancellationToken.None));

            // Act
            var unit = await handler.Handle(command);

            // Assert
            artistRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            artistRepositoryMock.Verify(r => r.DeleteAsync(It.IsAny<int>(), CancellationToken.None), Times.Never);
        }
    }
}
