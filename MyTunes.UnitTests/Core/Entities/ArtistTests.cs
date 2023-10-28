using MyTunes.Core.Entities;
using MyTunes.UnitTests.Core.TestData;

namespace MyTunes.UnitTests.Core.Entities
{
    public class ArtistTests
    {
        public static IEnumerable<object[]> ValidInputData
            => ArtistTestsData.GetValidInputData();

        public static IEnumerable<object[]> InvalidInputData
            => ArtistTestsData.GetInvalidInputData();

        [Theory]
        [MemberData(nameof(ValidInputData))]
        public void InputDataIsValid_Executed_CreateNewArtist(string name, string biography)
        {
            // Arrange

            // Act
            var artist = new Artist(name, biography);

            // Assert
            Assert.Equal(name, artist.Name);
            Assert.Equal(biography, artist.Biography);
            Assert.Empty(artist.Albums);
        }

        [Theory]
        [MemberData(nameof(InvalidInputData))]
        public void InputDataIsNotValid_Executed_ThrowException(string name, string biography)
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAny<ArgumentException>(() => new Artist(name, biography));
        }

        [Theory]
        [MemberData(nameof(ValidInputData))]
        public void InputDataIsValid_Executed_UpdateArtist(string name, string biography)
        {
            // Arrange
            var artist = new Artist("name", "biography");

            // Act
            artist.Update(name, biography);

            // Assert
            Assert.Equal(name, artist.Name);
            Assert.Equal(biography, artist.Biography);
        }

        [Theory]
        [MemberData(nameof(InvalidInputData))]
        public void InputDataIsNotValid_Executed_ThrowExceptionForUpdateArtist(string name, string biography)
        {
            // Arrange
            var artist = new Artist("name", "biography");

            // Act

            // Assert
            Assert.ThrowsAny<ArgumentException>(() => artist.Update(name, biography));
        }
    }
}
