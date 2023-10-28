using AutoMapper;
using Moq;
using MyTunes.Application.Queries.GetUserById;
using MyTunes.Application.ViewModels.User;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Queries
{
    public class GetUserByIdQueryHandlerTests
    {
        [Fact]
        public async void UserExists_Executed_ReturnUser()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperMock = new Mock<IMapper>();
            var query = new GetUserByIdQuery(It.IsAny<int>());
            var handler = new GetUserByIdQueryHandler(userRepositoryMock.Object, mapperMock.Object);

            var user = new User("name", "email", "password", It.IsAny<UserRole>());
            var userViewModel = new UserViewModel("name","email", It.IsAny<UserRole>());

            userRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(user);
            mapperMock.Setup(m => m.Map<UserViewModel>(It.IsAny<User>())).Returns(userViewModel);

            // Act
            var returnedViewModel = await handler.Handle(query);

            // Assert
            Assert.Equal(userViewModel, returnedViewModel);
            userRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            mapperMock.Verify(m => m.Map<UserViewModel>(It.IsAny<User>()), Times.Once);
        }
    }
}
