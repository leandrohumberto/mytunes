using Moq;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.UnitTests.Core.TestData;

namespace MyTunes.UnitTests.Core.Entities
{
    public class UserTests
    {
        public static IEnumerable<object[]> ValidInputDataForNewUser
            => UserTestsData.GetValidInputDataForNewUser();
        public static IEnumerable<object[]> InvalidInputDataForNewUser
            => UserTestsData.GetInvalidInputDataForNewUser();

        [Theory]
        [MemberData(nameof(ValidInputDataForNewUser))]
        public void InputDataIsValid_Executed_CreateNewUser(string name, string email, string password, UserRole role)
        {
            // Arrange

            // Act
            var user = new User(name, email, password, role);

            // Assert
            Assert.Equal(name, user.Name);
            Assert.Equal(email, user.Email);
            Assert.Equal(password, user.Password);
            Assert.Equal(role, user.Role);
        }

        [Theory]
        [MemberData(nameof(InvalidInputDataForNewUser))]
        public void InputDataIsNotValid_Executed_ThrowExceptionForNewUser(string name, string email, string password, UserRole role)
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAny<ArgumentException>(() => new User(name, email, password, role));
        }

        [Theory]
        [InlineData("newPassword")]
        public void NewPasswordIsValid_Executed_UpdatePassword(string newPassword)
        {
            // Arrange
            var user = new User("name", "email", "password", It.IsAny<UserRole>());

            // Act
            user.ChangePassword(newPassword);

            // Assert
            Assert.Equal(newPassword, user.Password);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void NewPasswordIsNotValid_Executed_ThrowExceptionForUpdatePassword(string newPassword)
        {
            // Arrange
            var user = new User("name", "email", "password", It.IsAny<UserRole>());

            // Act

            // Assert
            Assert.ThrowsAny<ArgumentException>(() => user.ChangePassword(newPassword));
        }
    }
}
