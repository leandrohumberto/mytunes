using MyTunes.Core.Entities;
using MyTunes.Core.Enums;

namespace MyTunes.UnitTests.Core.TestData
{
    public static class AlbumTestsData
    {
        private static readonly List<Track> _validTracklist = new()
        {
            new Track(1, "Track 01", TimeSpan.FromSeconds(180)),
            new Track(2, "Track 02", TimeSpan.FromSeconds(180)),
        };

        private static readonly List<Track> _invalidTracklist = new()
        {
            new Track(1, "Track 01", TimeSpan.FromSeconds(180)),
            new Track(3, "Track 03", TimeSpan.FromSeconds(180)),
        };

        private static readonly List<Track> _emptyTracklist = new();

        public static IEnumerable<object[]> GetValidInputData()
        {
            yield return new object[]
            {
                "Opus Eponymous", 2010U, "Metal", AlbumFormat.FullLength, _validTracklist
            };
        }

        public static IEnumerable<object[]> GetInvalidInputData()
        {
            // Nome do álbum vazio
            yield return new object[] { string.Empty, 2000, "Rock", AlbumFormat.FullLength, _validTracklist };

            // Ano zero
            yield return new object[] { "Album", 0U, "Rock", AlbumFormat.FullLength, _validTracklist };

            // Gênero do álbum vazio
            yield return new object[] { "Album", 2000U, string.Empty, AlbumFormat.FullLength, _validTracklist };

            // Lista de faixas com numeração inválida
            yield return new object[] { "Album", 2000U, "Rock", AlbumFormat.FullLength, _invalidTracklist };

            // Lista de faixas vazia
            yield return new object[] { "Album", 2000U, "Rock", AlbumFormat.FullLength, _emptyTracklist };

            // Lista de faixas com referência nula
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            yield return new object[] { "Album", 2000U, "Rock", AlbumFormat.FullLength, null };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}
