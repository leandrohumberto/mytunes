using MyTunes.Core.Enums;

namespace MyTunes.UnitTests.Core.TestData
{
    public static class UserTestsData
    {
        public static IEnumerable<object[]> GetValidInputDataForNewUser()
        {
            yield return new object[] { "Usuário 01", "mail@mail.com", "1234", UserRole.Admin, };
            yield return new object[] { "Usuário 02", "mail01@mail.com", "5678", UserRole.Viewer, };
        }

        public static IEnumerable<object[]> GetInvalidInputDataForNewUser()
        {
            // Nome em branco
            yield return new object[] { string.Empty, "mail@mail.com", "1234", UserRole.Admin, };

            // E-mail em branco
            yield return new object[] { "Usuário 02", string.Empty, "5678", UserRole.Viewer, };

            // Senha em branco
            yield return new object[] { "Usuário 03", "mail03@mail.com", string.Empty, UserRole.Viewer, };

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

            // Nome nulo
            yield return new object[] { null, "mail04@mail.com", "1234", UserRole.Admin, };

            // E-mail nulo
            yield return new object[] { "Usuário 05", null, "5678", UserRole.Viewer, };

            // Senha nula
            yield return new object[] { "Usuário 06", "mail06@mail.com", null, UserRole.Viewer, };

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.
        }
    }
}
