using CryptoHelper;

namespace Infrastructure.Services.Common
{

    /// <summary>
    /// https://github.com/henkmollema/CryptoHelper
    /// </summary>
    public static class Password
    {
        public static string HashPassword(string password)
        {
            return Crypto.HashPassword(password);
        }

        public static bool VerifyPassword(string hash, string password)
        {
            return Crypto.VerifyHashedPassword(hash, password);
        }
    }
}
