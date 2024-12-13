using Microsoft.IdentityModel.Tokens;
using SecurityWebhook.Lib.Models.Constants;
using SecurityWebhook.Lib.Models.ContributorModels;
using SecurityWebhook.Lib.Repository.UserRepos;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SecurityWebhoook.Lib.Services.AuthServices
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepo _userRepo;

        public AuthService(IUserRepo userRepo)
        {

            _userRepo = userRepo;
        }

        public async Task<ContributorAuthResponse> CreateUserAsync(ContributorAuth contributorAuth)
        {
            var userId = await _userRepo.CreateUserAsync(contributorAuth);
            ContributorAuthResponse authResponse = new ContributorAuthResponse();
            if(userId > 0)
            {
                authResponse.AuthEnums = SecurityWebhook.Lib.Models.Enums.AuthEnums.Success;
                authResponse.Message = AuthConstants.RegistrationSuccess;
            }
            else
            {
                authResponse.AuthEnums = SecurityWebhook.Lib.Models.Enums.AuthEnums.AlreadyExists;
                authResponse.Message = AuthConstants.UserAlreadyExists;
            }



            return authResponse;


        }

        public async Task<UserLoginInfo> UserLoginAsync(ContributorAuth contributorAuth)
        {
            UserLoginInfo loginInfo = new UserLoginInfo();
            var response = await _userRepo.UserLoginAsync(contributorAuth);
            if (response != null)
            {
                if (!string.IsNullOrEmpty(response.Password)) 
                {
                    loginInfo.Status = SecurityWebhook.Lib.Models.Enums.AuthEnums.Success;
                    loginInfo.Message = "Success";
                    Dictionary<string,string> claims = new Dictionary<string,string>();
                    claims.Add("UKI", response.UserId);
                    claims.Add("UEM", contributorAuth.Email);
                    claims.Add("AT", response.APIToken);
                    var token = CreateJwt("SecurityWebhookAPI", "SSCPWeb",AuthConstants.JWTKey,claims, TimeSpan.FromDays(1));
                    loginInfo.Token = token;
                }

                else
                {
                    loginInfo.Status = SecurityWebhook.Lib.Models.Enums.AuthEnums.Failed;
                    loginInfo.Message = "Input username or password is incorrect!";
                }
            }
            else
            {
                loginInfo.Status = SecurityWebhook.Lib.Models.Enums.AuthEnums.DoesNotExist;
                loginInfo.Message = "User does not exist!";
            }
            return loginInfo;
        }

        public static string CreateJwt(
        string issuer,
        string audience,
        string secretKey,
        Dictionary<string, string> claims,
        TimeSpan expiryDuration)
        {
            // Create the signing credentials using the secret key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Add claims
            var tokenClaims = new List<Claim>();
            foreach (var claim in claims)
            {
                tokenClaims.Add(new Claim(claim.Key, claim.Value));
            }

            // Create the JWT token
            var jwtToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: tokenClaims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(expiryDuration),
                signingCredentials: signingCredentials);

            // Generate the token string
            return new JwtSecurityTokenHandler().WriteToken(jwtToken);
        }
    }
}

