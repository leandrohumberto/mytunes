using Moq;
using MyTunes.Application.Commands.UpdateArtist;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Commands
{
    public class UpdateArtistCommandHandlerTests
    {
        [Fact]
        public async void ArtistExists_Executed_UpdateArtist()
        {
            // Arrange
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var command = new UpdateArtistCommand("name", It.IsAny<string>());
            command.SetId(It.IsAny<int>());
            var handler = new UpdateArtistCommandHandler(artistRepositoryMock.Object);

            _ = artistRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(true);
            _ = artistRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(new Artist("name", "bio"));

            // Act
            await handler.Handle(command);

            // Assert
            artistRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            artistRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
        }

        [Fact]
        public async void ArtisDoesNottExist_Executed_DoNotUpdateArtist()
        {
            // Arrange
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var command = new UpdateArtistCommand("name", It.IsAny<string>());
            command.SetId(It.IsAny<int>());
            var handler = new UpdateArtistCommandHandler(artistRepositoryMock.Object);

            _ = artistRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(false);

            // Act
            await handler.Handle(command);

            // Assert
            artistRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            artistRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Never);
        }
    }
}
