using AutoMapper;
using Moq;
using MyTunes.Application.Queries.GetArtists;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Queries
{
    public class GetArtistsQueryHandlerTests
    {
        [Fact]
        public async void AnyArtistFound_Executed_ReturnFoundArtists()
        {
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var mapperMock = new Mock<IMapper>();
            var query = new GetArtistsQuery();
            var handler = new GetArtistsQueryHandler(artistRepositoryMock.Object, mapperMock.Object);

            var artist = new Artist("name","bio");
            var artists = Enumerable.Empty<Artist>().Append(artist);
            var artistViewModel = new ArtistViewModel(It.IsAny<int>(), "name", "artist");

            artistRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<string>(), CancellationToken.None).Result)
                .Returns(artists);
            mapperMock.Setup(m => m.Map<ArtistViewModel>(It.IsAny<Artist>())).Returns(artistViewModel);

            // Act
            var returnedAlbums = await handler.Handle(query);

            // Assert
            Assert.Contains(artistViewModel, returnedAlbums);
            artistRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<string>(), CancellationToken.None).Result,
                Times.Once);
            mapperMock.Verify(m => m.Map<ArtistViewModel>(It.IsAny<Artist>()), Times.AtLeastOnce);
        }

        [Fact]
        public async void NoArtistsFound_Executed_ReturnNoArtists()
        {
            // Arrange
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var mapperMock = new Mock<IMapper>();
            var query = new GetArtistsQuery();
            var handler = new GetArtistsQueryHandler(artistRepositoryMock.Object, mapperMock.Object);

            artistRepositoryMock.Setup(r => r.GetAllAsync(It.IsAny<string>(), CancellationToken.None).Result)
                .Returns(Enumerable.Empty<Artist>());
            mapperMock.Setup(m => m.Map<ArtistViewModel>(It.IsAny<Artist>()));

            // Act
            var returnedAlbums = await handler.Handle(query);

            // Assert
            Assert.Empty(returnedAlbums);
            artistRepositoryMock.Verify(r => r.GetAllAsync(It.IsAny<string>(), CancellationToken.None).Result,
                Times.Once);
            mapperMock.Verify(m => m.Map<ArtistViewModel>(It.IsAny<Artist>()), Times.Never);
        }
    }
}
