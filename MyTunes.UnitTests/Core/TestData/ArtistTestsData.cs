namespace MyTunes.UnitTests.Core.TestData
{
    public static class ArtistTestsData
    {
        public static IEnumerable<object[]> GetValidInputData()
        {
            yield return new object[] { "Ghost", "Biography" };
            yield return new object[] { "Trivium", string.Empty };
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            yield return new object[] { "Mastodon", null };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }

        public static IEnumerable<object[]> GetInvalidInputData()
        {
            yield return new object[] { string.Empty, string.Empty };
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            yield return new object[] { null, null };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}
