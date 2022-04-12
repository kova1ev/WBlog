using System.Text;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace WBlog.Infrastructure.Extantions
{
    public static class StringExtantions
    {

        public static string CreateHash(this string password, string salt)
        {
            if (string.IsNullOrWhiteSpace(salt))
                throw new ArgumentNullException();
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);
            byte[] hash = KeyDerivation.Pbkdf2(
                            password: password,
                            salt: saltBytes,
                            prf: KeyDerivationPrf.HMACSHA256,
                            iterationCount: 100000,
                            numBytesRequested: 256 / 8);
            return Convert.ToBase64String(hash);
        }
    }
}