using ReservationApi.Models.Entities;
using ReservationApi.Models.Shared;

namespace ReservationApi.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<UserEntity?> GetUserByEmailAsync(string email);
        Task<RefreshTokenModel> GenerateRefrsehToken(string email);
    }
}
