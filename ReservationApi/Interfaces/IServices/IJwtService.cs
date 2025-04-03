using ReservationApi.Models.Entities;
using ReservationApi.Models.Enuns;
using ReservationApi.Models.Shared;

namespace ReservationApi.Interfaces.IServices
{
    public interface IJwtService
    {
        string GenerateToken(RoleEnum role, string email, Guid idClient);
        Task<RefreshTokenModel> GenerateRefreshToken(string email);
        Task<UserEntity?> ValidateRefreshToken(Guid id, string refreshToken);
    }
}
