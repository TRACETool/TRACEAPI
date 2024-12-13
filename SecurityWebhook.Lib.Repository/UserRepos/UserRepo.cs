using Mapster;
using Microsoft.EntityFrameworkCore;
using SecurityWebhook.Lib.Models.ContributorModels;
using SecurityWebhook.Lib.Models.GithubModels;
using SecurityWebhook.Lib.Repository.Entities;
using System.Security.Cryptography;

namespace SecurityWebhook.Lib.Repository.UserRepos
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context) 
        {
            _context = context;        
        }

        public async Task<long> CreateUserAsync(ContributorAuth contributorAuth)
        {
            long contributorId = 0;
            var record = await _context.ContributorMaster.SingleOrDefaultAsync(x => x.ContributorEmail == contributorAuth.Email);
            if (record == null) 
            {
                ContributorMaster contributorMaster = new();
                contributorMaster.ContributorEmail = contributorAuth.Email;
                contributorMaster.ContributorName = contributorAuth.Name;
                contributorMaster.UserId = contributorAuth.UserId;
                contributorMaster.PasswordSalt = GenerateRandomSalt(12);
                contributorMaster.Password = Convert.ToBase64String(HashPassword(contributorAuth.Password,contributorMaster.PasswordSalt));
                contributorMaster.APIToken = contributorAuth.APIToken;
                await _context.ContributorMaster.AddAsync(contributorMaster);
                await _context.SaveChangesAsync();
                contributorId = contributorMaster.ContributorId;


            }
            return contributorId;

        }

        public async Task<ContributorAuth> UserLoginAsync(ContributorAuth auth)
        {
            var email = auth.Email;
            var record = await _context.ContributorMaster.SingleOrDefaultAsync(x => x.ContributorEmail.Equals(email));
            if (record != null) 
            {
                var passwordSalt = record.PasswordSalt;
                var password = Convert.ToBase64String(HashPassword(auth.Password, passwordSalt));
                if(record.Password == password)
                {
                    return record.Adapt<ContributorAuth>();
                }
                else
                {
                    return new ContributorAuth();
                }
            }
            return null;
        }

        public async Task<ContributorAuth> GetContributorAsync(string email)
        {
            var record = await _context.ContributorMaster.SingleOrDefaultAsync(x => x.ContributorEmail == email);
            var contributor = record.Adapt<ContributorAuth>();
            return contributor;
        }

        public static string GenerateRandomPassword(int length)
        {
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=<>?";
            var randomBytes = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(randomBytes);
            }

            var passwordChars = new char[length];
            for (int i = 0; i < length; i++)
            {
                passwordChars[i] = validChars[randomBytes[i] % validChars.Length];
            }

            return new string(passwordChars);
        }

        // Method to generate a random salt (as a base64-encoded string)
        public static string GenerateRandomSalt(int length)
        {
            var salt = new byte[length];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt); // Convert salt to base64 string
        }

        // Method to hash the password with the salt using PBKDF2
        public static byte[] HashPassword(string password, string base64Salt)
        {
            // Convert base64 salt back to byte array
            byte[] salt = Convert.FromBase64String(base64Salt);

            // Hash the password using PBKDF2 with the salt
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000, HashAlgorithmName.SHA256))
            {
                return pbkdf2.GetBytes(32); // 32 bytes output (256 bits)
            }
        }
    }
}

