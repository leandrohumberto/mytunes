using AutoMapper;
using Moq;
using MyTunes.Application.Queries.GetArtistById;
using MyTunes.Application.ViewModels.Artist;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Queries
{
    public class GetArtistByIdQueryHandlerTests
    {
        [Fact]
        public async void ArtistExists_Executed_ReturnArtist()
        {
            // Arrange
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var mapperMock = new Mock<IMapper>();
            var query = new GetArtistByIdQuery(It.IsAny<int>());
            var handler = new GetArtistByIdQueryHandler(artistRepositoryMock.Object, mapperMock.Object);

            var artist = new Artist("name", "biography");
            var artistViewModel = new ArtistViewModel(It.IsAny<int>(), artist.Name, artist.Biography);

            artistRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result)
                .Returns(artist);
            mapperMock.Setup(m => m.Map<ArtistViewModel>(It.IsAny<Artist>())).Returns(artistViewModel);

            // Act
            var returnedViewModel = await handler.Handle(query);

            // Assert
            Assert.Equal(artistViewModel, returnedViewModel);
            artistRepositoryMock.Verify(r => r.GetByIdAsync(It.IsAny<int>(), CancellationToken.None).Result,
                Times.Once);
            mapperMock.Verify(m => m.Map<ArtistViewModel>(It.IsAny<Artist>()), Times.Once);
        }
    }
}
