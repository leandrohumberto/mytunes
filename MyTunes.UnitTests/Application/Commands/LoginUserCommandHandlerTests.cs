using Moq;
using MyTunes.Application.Commands.LoginUser;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.Core.Exceptions;
using MyTunes.Core.Repositories;
using MyTunes.Core.Services;

namespace MyTunes.UnitTests.Application.Commands
{
    public class LoginUserCommandHandlerTests
    {
        [Fact]
        public async void ValidEmailAndPassword_Executed_AuthenticateUser()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var command = new LoginUserCommand(It.IsAny<string>(), It.IsAny<string>());
            var handler = new LoginUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object);

            _ = authServiceMock.Setup(s => s.ComputeSha256Hash(It.IsAny<string>())).Returns(It.IsAny<string>());
            _ = authServiceMock.Setup(s => s.GenerateJwtToken(It.IsAny<string>(), It.IsAny<UserRole>()))
                .Returns(It.IsAny<string>());
            _ = userRepositoryMock.Setup(r => r.GetByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None).Result)
                .Returns(new User("name", "email", "password", UserRole.Admin));

            // Act
            var viewModel = await handler.Handle(command);

            // Assert
            Assert.NotNull(viewModel);
            authServiceMock.Verify(s => s.ComputeSha256Hash(It.IsAny<string>()), Times.Once);
            authServiceMock.Verify(s => s.GenerateJwtToken(It.IsAny<string>(), It.IsAny<UserRole>()), Times.Once);
            userRepositoryMock.Verify(r => r.GetByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None).Result,
                Times.Once);
        }


        [Fact]
        public void InvalidEmailAndPassword_Executed_ThrowException()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            var userRepositoryMock = new Mock<IUserRepository>();
            var command = new LoginUserCommand(It.IsAny<string>(), It.IsAny<string>());
            var handler = new LoginUserCommandHandler(userRepositoryMock.Object, authServiceMock.Object);

            _ = authServiceMock.Setup(s => s.ComputeSha256Hash(It.IsAny<string>())).Returns(It.IsAny<string>());
            _ = authServiceMock.Setup(s => s.GenerateJwtToken(It.IsAny<string>(), It.IsAny<UserRole>()))
                .Returns(It.IsAny<string>());
            _ = userRepositoryMock.Setup(r => r.GetByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None).Result)
                .Returns(() => null);

            // Act

            // Assert
            _ = Assert.ThrowsAnyAsync<LoginFailException>(() => handler.Handle(command));
            authServiceMock.Verify(s => s.ComputeSha256Hash(It.IsAny<string>()), Times.Once);
            authServiceMock.Verify(s => s.GenerateJwtToken(It.IsAny<string>(), It.IsAny<UserRole>()),
                Times.Never);
            userRepositoryMock.Verify(r => r.GetByEmailAndPasswordAsync(It.IsAny<string>(), It.IsAny<string>(), CancellationToken.None).Result,
                Times.Once);
        }
    }
}
