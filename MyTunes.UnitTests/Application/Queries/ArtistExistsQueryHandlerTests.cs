using Moq;
using MyTunes.Application.Queries.ArtistExists;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Queries
{
    public class ArtistExistsQueryHandlerTests
    {
        [Fact]
        public async void ArtistExists_Executed_ReturnTrue()
        {
            // Arrange
            var mock = new Mock<IArtistRepository>();
            var query = new ArtistExistsQuery(It.IsAny<int>());
            var handler = new ArtistExistsQueryHandler(mock.Object);
            mock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result).Returns(true);

            // Act
            var exists = await handler.Handle(query);

            // Assert
            Assert.True(exists);
            mock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void ArtistDoesNotExist_Executed_ReturnFalse()
        {
            // Arrange
            var mock = new Mock<IArtistRepository>();
            var query = new ArtistExistsQuery(It.IsAny<int>());
            var handler = new ArtistExistsQueryHandler(mock.Object);
            mock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result).Returns(false);

            // Act
            var exists = await handler.Handle(query);

            // Assert
            Assert.False(exists);
            mock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None), Times.Once);
        }
    }
}
