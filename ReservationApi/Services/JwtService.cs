using Microsoft.IdentityModel.Tokens;
using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Enuns;
using ReservationApi.Models.Shared;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ReservationApi.Services
{
    public class JwtService : IJwtService
    {
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly int _expirationMinutes;
        private readonly IAuthRepository _authRepository;
        private readonly IBaseRepository<UserEntity> _baseUserRepository;

        public JwtService(IConfiguration configuration, IAuthRepository authRepository, IBaseRepository<UserEntity> baseUserRepository)
        {
            _baseUserRepository = baseUserRepository;
            _authRepository = authRepository;
            var jwtSettings = configuration.GetSection("JwtSettings");
            _secretKey = jwtSettings["Key"] ?? string.Empty;
            _issuer = jwtSettings["Issuer"] ?? string.Empty;
            _audience = jwtSettings["Audience"] ?? string.Empty;
            _expirationMinutes = int.Parse(jwtSettings["ExpirationMinutes"] ?? "60");
        }

        public async Task<RefreshTokenModel> GenerateRefreshToken(string email)
        {
            try
            {
                RefreshTokenModel? refreshToken = await _authRepository.GenerateRefrsehToken(email);

                if (refreshToken == null || refreshToken.RefreshToken == null)
                {
                    throw new Exception("Refresh token generation failed");
                }
                return refreshToken;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public string GenerateToken(RoleEnum role, string name, Guid idClient)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_secretKey);

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity([
                        new Claim(ClaimTypes.Name, name),
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

        public async Task<UserEntity?> ValidateRefreshToken(Guid id, string refreshToken)
        {
            try
            {
                UserEntity? user = await _baseUserRepository.GetByIdAsync(id);
                bool isExpiretedRefreshToken = new DateOnly() > user?.ExpirationDateRefreshToken;
                bool isDifferentToken = refreshToken != user?.RefreshToken;
                if (user == null) throw new Exception("Nonexistent user");
                if (isDifferentToken || isExpiretedRefreshToken)
                {
                    await _authRepository.DeleteRefrsehToken(id);
                    return null;
                }
                return user;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
