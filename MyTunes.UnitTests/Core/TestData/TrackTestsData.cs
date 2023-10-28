namespace MyTunes.UnitTests.Core.TestData
{
    public static class TrackTestsData
    {
        public static IEnumerable<object[]> GetValidInputData()
        {
            yield return new object[] { 1, "Deus Culpa", TimeSpan.FromSeconds(35), };
            yield return new object[] { 2, "ConClavi Con Dio", TimeSpan.FromSeconds(250), };
            yield return new object[] { 3, "Ritual", TimeSpan.FromSeconds(263), };
        }

        public static IEnumerable<object[]> GetInvalidInputData()
        {
            yield return new object[] { 0, "Deus Culpa", TimeSpan.FromSeconds(35), };
            yield return new object[] { 2, string.Empty, TimeSpan.FromSeconds(250), };
            yield return new object[] { 3, "Ritual", TimeSpan.FromSeconds(0), };
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
            yield return new object[] { 4, null, TimeSpan.FromSeconds(273), };
#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}
