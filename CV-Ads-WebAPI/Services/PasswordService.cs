using System;
using System.Security.Cryptography;

namespace CV_Ads_WebAPI.Services
{
    public class PasswordService
    {
        private const int ITERATIONS_COUNT = 1000;
        private const int SALT_SIZE = 0x10;
        private const int HASHED_PASSWORD_SIZE = 0x20;

        public string GeneratePassword(string password)
        {
            byte[] salt;
            byte[] hashedPassword;

            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(
                password, SALT_SIZE, ITERATIONS_COUNT))
            {
                salt = bytes.Salt;
                hashedPassword = bytes.GetBytes(HASHED_PASSWORD_SIZE);
            }

            byte[] saltAndHashedPassword = new byte[SALT_SIZE + HASHED_PASSWORD_SIZE];
            Buffer.BlockCopy(salt, 0, saltAndHashedPassword, 0, SALT_SIZE);
            Buffer.BlockCopy(hashedPassword, 0, saltAndHashedPassword, SALT_SIZE, HASHED_PASSWORD_SIZE);

            return Convert.ToBase64String(saltAndHashedPassword);
        }

        public bool VerifyHashedPassword(string saltAndHashedPasswordString, string password)
        {
            byte[] saltAndHashedPassword = Convert.FromBase64String(saltAndHashedPasswordString);
            if (saltAndHashedPassword.Length != SALT_SIZE + HASHED_PASSWORD_SIZE)
            {
                return false;
            }

            byte[] salt = new byte[SALT_SIZE];
            Buffer.BlockCopy(saltAndHashedPassword, 0, salt, 0, SALT_SIZE);

            byte[] hashedPassword = new byte[HASHED_PASSWORD_SIZE];
            Buffer.BlockCopy(saltAndHashedPassword, SALT_SIZE, hashedPassword, 0, HASHED_PASSWORD_SIZE);

            byte[] givenPasswordHash;
            using (Rfc2898DeriveBytes bytes = new Rfc2898DeriveBytes(password, salt, ITERATIONS_COUNT))
            {
                givenPasswordHash = bytes.GetBytes(HASHED_PASSWORD_SIZE);
            }
            return ByteArraysEqual(hashedPassword, givenPasswordHash);
        }

        private static bool ByteArraysEqual(byte[] a1, byte[] a2)
        {
            if (a1.Length != a2.Length)
                return false;

            for (int i = 0; i < a1.Length; i++)
                if (a1[i] != a2[i])
                    return false;

            return true;
        }
    }
}
