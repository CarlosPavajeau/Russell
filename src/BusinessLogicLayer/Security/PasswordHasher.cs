using System;
using System.Security.Cryptography;
using System.Text;

namespace BusinessLogicLayer.Security
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            SHA256 sha256 = SHA256.Create();

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashedBytes);
        }

        public static bool VerifyPassword(string hashedPassword, string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);

            SHA256 sha256 = SHA256.Create();
            passwordBytes = sha256.ComputeHash(passwordBytes);

            return Convert.ToBase64String(passwordBytes) == hashedPassword;
        }
    }
}
