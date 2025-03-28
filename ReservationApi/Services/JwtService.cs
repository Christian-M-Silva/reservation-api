using Microsoft.IdentityModel.Tokens;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Enuns;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ReservationApi.Services
{
    public class JwtService:IJwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationMinutes;

        public JwtService(IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            _secretKey = jwtSettings["Key"] ?? string.Empty;
            _issuer = jwtSettings["Issuer"] ?? string.Empty;
            _audience = jwtSettings["Audience"] ?? string.Empty;
            _expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"] ?? "60");
        }

        public string GenerateRefreshToken(string email)
        {
            throw new NotImplementedException();
        }

        public string GenerateToken(RoleEnum role, string email, Guid idClient)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                    [
                    new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.NameIdentifier, idClient.ToString()),
                new Claim(ClaimTypes.Role, role.ToString())
                ]),
                    Expires = DateTime.UtcNow.AddMinutes(_expirationMinutes),
                    Issuer = _issuer,
                    Audience = _audience,
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha256Signature
                    )
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var jwtToken = tokenHandler.WriteToken(token);

                return jwtToken;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
