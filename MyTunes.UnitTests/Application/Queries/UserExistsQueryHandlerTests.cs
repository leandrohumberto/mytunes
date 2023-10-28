using Moq;
using MyTunes.Application.Queries.UserExists;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Queries
{
    public class UserExistsQueryHandlerTests
    {
        [Fact]
        public async void UserExists_Executed_ReturnTrue()
        {
            // Arrange
            var mock = new Mock<IUserRepository>();
            var query = new UserExistsQuery(It.IsAny<int>());
            var handler = new UserExistsQueryHandler(mock.Object);
            mock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result).Returns(true);

            // Act
            var exists = await handler.Handle(query);

            // Assert
            Assert.True(exists);
            mock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None), Times.Once);
        }

        [Fact]
        public async void UserDoesNotExist_Executed_ReturnFalse()
        {
            // Arrange
            var mock = new Mock<IUserRepository>();
            var query = new UserExistsQuery(It.IsAny<int>());
            var handler = new UserExistsQueryHandler(mock.Object);
            mock.Setup(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None).Result).Returns(false);

            // Act
            var exists = await handler.Handle(query);

            // Assert
            Assert.False(exists);
            mock.Verify(r => r.ExistsAsync(It.IsAny<int>(), CancellationToken.None), Times.Once);
        }
    }
}
