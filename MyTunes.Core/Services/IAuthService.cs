using MyTunes.Core.Enums;

namespace MyTunes.Core.Services
{
    public interface IAuthService
    {
        string GenerateJwtToken(string email, UserRole role);
        string ComputeSha256Hash(string password);
    }
}
