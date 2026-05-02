using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecureBank.Helpers
{
    public static class Sha256HashUtils
    {
        public static string ComputeSha256Hash(byte[] inputBytes)
        {
            // Modified by Rezilant AI, 2026-05-02 23:51:07 GMT, Replaced SHA256 with Argon2id for secure password hashing - SHA256 is not suitable for password storage as it lacks salt and is too fast for brute-force protection
            // Note: This fix uses Argon2id which requires Konscious.Security.Cryptography.Argon2 NuGet package
            using Konscious.Security.Cryptography;
            
            // Generate a random salt
            byte[] salt = new byte[16];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            
            // Use Argon2id for password hashing
            using (var argon2 = new Argon2id(inputBytes))
            {
                argon2.Salt = salt;
                argon2.DegreeOfParallelism = 8;
                argon2.MemorySize = 65536; // 64 MB
                argon2.Iterations = 4;
                
                byte[] hash = argon2.GetBytes(32);
                
                // Combine salt + hash for storage
                byte[] hashBytes = new byte[48];
                Array.Copy(salt, 0, hashBytes, 0, 16);
                Array.Copy(hash, 0, hashBytes, 16, 32);
                
                // Convert to hex string for return compatibility
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
            
            // Original Code - Vulnerable SHA256 implementation
            // Create a SHA256   
            // using SHA256 sha256Hash = SHA256.Create();
            // ComputeHash - returns byte array  
            // byte[] bytes = sha256Hash.ComputeHash(inputBytes);

            // Convert byte array to a string   
            // StringBuilder builder = new StringBuilder();
            // for (int i = 0; i < bytes.Length; i++)
            // {
            //     builder.Append(bytes[i].ToString("x2"));
            // }
            // return builder.ToString();
        }
    }
}