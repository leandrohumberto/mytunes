using AutoMapper;
using Moq;
using MyTunes.Application.Commands.CreateUser;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.Core.Exceptions;
using MyTunes.Core.Repositories;
using MyTunes.Core.Services;

namespace MyTunes.UnitTests.Application.Commands
{
    public class CreateUserCommandHandlerTests
    {
        [Fact]
        public async void EmailDoesNotExist_Executed_ReturnUserId()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();
            var mapperMock = new Mock<IMapper>();
            var command = new CreateUserCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<UserRole>());
            var handler = new CreateUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object, mapperMock.Object);
            var user = new User("name", "email", "password", UserRole.Admin);

            _ = userRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<string>(), CancellationToken.None).Result)
                .Returns(false);
            _ = mapperMock.Setup(m => m.Map<User>(command)).Returns(user);
            _ = authServiceMock.Setup(s => s.ComputeSha256Hash(It.IsAny<string>())).Returns("new password");

            // Act
            var id = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(id >= 0);
            userRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<string>(), CancellationToken.None).Result,
                Times.Once);
            authServiceMock.Verify(s => s.ComputeSha256Hash(It.IsAny<string>()), Times.Once);
            mapperMock.Verify(m => m.Map<User>(command), Times.Once);
        }

        [Fact]
        public async void EmailExists_Executed_ThrowException()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var authServiceMock = new Mock<IAuthService>();
            var mapperMock = new Mock<IMapper>();
            var command = new CreateUserCommand(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<UserRole>());
            var handler = new CreateUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object, mapperMock.Object);
            var user = new User("name", "email", "password", UserRole.Admin);

            userRepositoryMock.Setup(r => r.ExistsAsync(It.IsAny<string>(), CancellationToken.None).Result)
                .Returns(true);

            // Act

            // Assert
            _ = await Assert.ThrowsAnyAsync<InvalidUserEmailException>(() => handler.Handle(command, CancellationToken.None));
            userRepositoryMock.Verify(r => r.ExistsAsync(It.IsAny<string>(), CancellationToken.None).Result,
                Times.Once);
            authServiceMock.Verify(s => s.ComputeSha256Hash(It.IsAny<string>()), Times.Never);
            mapperMock.Verify(m => m.Map<User>(command), Times.Never);
        }
    }
}
