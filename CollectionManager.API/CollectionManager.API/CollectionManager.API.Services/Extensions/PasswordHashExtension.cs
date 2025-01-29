using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CollectionManager.API.Services.Extensions
{
    public static class PasswordHashExtension
    {
        public static (string hash, string salt) HashPassword(this string password)
        {
            byte[] salt = GenerateSalt();
            byte[] hashBytes = HashPasswordWithSalt(password, salt);
            string hash = Convert.ToBase64String(hashBytes);
            string saltString = Convert.ToBase64String(salt);
            return (hash, saltString);
        }


        public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = Convert.FromBase64String(storedSalt);

            byte[] enteredHashBytes = HashPasswordWithSalt(enteredPassword, salt);

            if (hashBytes.Length != enteredHashBytes.Length)
            {
                return false;
            }

            return SlowEquals(hashBytes, enteredHashBytes);
        }

        private static byte[] GenerateSalt()
        {
            byte[] salt = new byte[16];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] HashPasswordWithSalt(string password, byte[] salt)
        {
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 1000, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32);
            }
        }

        private static bool SlowEquals(byte[] enteredBytes, byte[] knownBytes)
        {
            int diff = enteredBytes.Length ^ knownBytes.Length;
            for (int i = 0; i < enteredBytes.Length && i < knownBytes.Length; i++)
            {
                diff |= enteredBytes[i] ^ knownBytes[i];
            }
            return diff == 0;
        }
    }
}
