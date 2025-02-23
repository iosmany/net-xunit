using ErrorOr;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetXUnit.Webapi.Services.Implementations
{
    sealed class JwtTokenManager
    {

        const string secretKey = "salt4@kl#xunittesting24!secretkp";

        public JwtTokenManager() { }

        public ErrorOr<string> Generate(string source, string claimType= ClaimTypes.Email)
        {
            if(string.IsNullOrEmpty(source))
                return Error.Validation(description: "Email cannot be null or empty").ToMultiple();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secretKey); // Replace with your secret key
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(claimType, source)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
