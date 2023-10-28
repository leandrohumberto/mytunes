using Moq;
using MyTunes.Application.Queries.AlbumExists;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Queries
{
    public class AlbumExistsQueryHandlerTests
    {
        [Fact]
        public async void AlbumsExists_Executed_ReturnTrue()
        {
            // Arrange
            var mock = new Mock<IAlbumRepository>();
            var query = new AlbumExistsQuery(It.IsAny<int>());
            var handler = new AlbumExistsQueryHandler(mock.Object);
            mock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result).Returns(true);

            // Act
            var exists = await handler.Handle(query);

            // Assert
            Assert.True(exists);
            mock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void AlbumDoesNotExist_Executed_ReturnFalse()
        {
            //Arrange
            var mock = new Mock<IAlbumRepository>();
            var query = new AlbumExistsQuery(It.IsAny<int>());
            var handler = new AlbumExistsQueryHandler(mock.Object);
            mock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result).Returns(false);

            // Act
            var exists = await handler.Handle(query);

            // Assert
            Assert.False(exists);
            mock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None), Times.Once);
        }
    }
}
