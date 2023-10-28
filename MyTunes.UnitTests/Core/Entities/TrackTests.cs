using MyTunes.Core.Entities;
using MyTunes.UnitTests.Core.TestData;

namespace MyTunes.UnitTests.Core.Entities
{
    public class TrackTests
    {
        public static IEnumerable<object[]> ValidInputData =>
            TrackTestsData.GetValidInputData();
        public static IEnumerable<object[]> InvalidInputData =>
            TrackTestsData.GetInvalidInputData();

        [Theory]
        [MemberData(nameof(ValidInputData))]
        public void InputDataIsValid_Executed_CreateNewTrack(uint number, string title, TimeSpan length)
        {
            // Arrange

            // Act
            var track = new Track(number, title, length);

            // Assert
            Assert.Equal(number, track.Number);
            Assert.Equal(title, track.Title);
            Assert.Equal(length, track.Length);
        }

        [Theory]
        [MemberData(nameof(InvalidInputData))]
        public void InputDataIsNotValid_Executed_ThrowException(uint number, string title, TimeSpan length)
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAny<ArgumentException>(() => new Track(number, title, length));
        }
    }
}
