using AutoMapper;
using Moq;
using MyTunes.Application.Commands.CreateArtist;
using MyTunes.Core.Entities;
using MyTunes.Core.Repositories;

namespace MyTunes.UnitTests.Application.Commands
{
    public class CreateArtistCommandHandlerTests
    {
        [Fact]
        public async void ValidInputData_Executed_ReturnArtistId()
        {
            // Arrange
            var artistRepositoryMock = new Mock<IArtistRepository>();
            var mapperMock = new Mock<IMapper>();
            var command = new CreateArtistCommand(It.IsAny<string>(), It.IsAny<string>());
            var handler = new CreateArtistCommandHandler(artistRepositoryMock.Object, mapperMock.Object);

            _ = artistRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Artist>(), CancellationToken.None).Result)
                .Returns(It.IsAny<int>());

            // Act
            var id = await handler.Handle(command);

            // Assert
            Assert.True(id >= 0);
            artistRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Artist>(), CancellationToken.None).Result,
                Times.Once);
            mapperMock.Verify(m => m.Map<Artist>(command), Times.Once);
        }
    }
}
