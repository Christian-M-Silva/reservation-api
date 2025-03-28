using ReservationApi.Models.Enuns;

namespace ReservationApi.Interfaces.IServices
{
    public interface IJwtService
    {
        string GenerateToken(RoleEnum role, string email, Guid idClient);
        string GenerateRefreshToken(string email);

    }
}
