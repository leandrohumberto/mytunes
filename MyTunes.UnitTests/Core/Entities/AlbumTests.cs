﻿using Moq;
using MyTunes.Core.Entities;
using MyTunes.Core.Enums;
using MyTunes.UnitTests.Core.TestData;

namespace MyTunes.UnitTests.Core.Entities
{
    public class AlbumTests
    {
        public static IEnumerable<object[]> ValidInputData
            => AlbumTestsData.GetValidInputData();

        public static IEnumerable<object[]> InvalidInputData
            => AlbumTestsData.GetInvalidInputData();

        [Theory]
        [MemberData(nameof(ValidInputData))]
        public void InputDataIsValid_Executed_CreateNewAlbum(string title, uint year, string genre, AlbumFormat albumFormat, IEnumerable<Track> tracklist)
        {
            // Arrange

            // Act
            var album = new Album(title, It.IsAny<int>(), year, genre, albumFormat, tracklist);

            // Assert
            Assert.Equal(title, album.Title);
            Assert.Equal(year, album.Year);
            Assert.Equal(genre, album.Genre);
            Assert.Equal(albumFormat, album.Format);
            Assert.Equal(tracklist, album.Tracklist);
        }

        [Theory]
        [MemberData(nameof(InvalidInputData))]
        public void InputDataIsNotValid_Executed_ThrowExceptionForNewAlbum(string title, uint year, string genre, AlbumFormat albumFormat, IEnumerable<Track> tracklist)
        {
            // Arrange

            // Act

            // Assert
            Assert.ThrowsAny<ArgumentException>(() => new Album(title, It.IsAny<int>(), year, genre, albumFormat, tracklist));
        }

        [Theory]
        [MemberData(nameof(ValidInputData))]
        public void InputDataIsValid_Executed_UpdateAlbum(string title, uint year, string genre, AlbumFormat albumFormat, IEnumerable<Track> tracklist)
        {
            // Arrange
            var album = new Album("name", 1, 1U, "genre", AlbumFormat.Ep);

            // Act
            album.Update(title, year, genre, albumFormat, tracklist);

            // Assert
            Assert.Equal(title, album.Title);
            Assert.Equal(year, album.Year);
            Assert.Equal(genre, album.Genre);
            Assert.Equal(albumFormat, album.Format);
            Assert.Equal(tracklist, album.Tracklist);
        }

        [Theory]
        [MemberData(nameof(InvalidInputData))]
        public void InputDataIsNotValid_Executed_ThrowExceptionForAlbumUpdate(string title, uint year, string genre, AlbumFormat albumFormat, IEnumerable<Track> tracklist)
        {
            // Arrange
            var album = new Album("name", 1, 1U, "genre", AlbumFormat.Ep);

            // Act

            // Assert
            Assert.ThrowsAny<ArgumentException>(() => album.Update(title, year, genre, albumFormat, tracklist));
        }
    }
}
