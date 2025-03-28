using ReservationApi.Models.Entities;

namespace ReservationApi.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<UserEntity> GetUserByEmailAsync(string email);

    }
}
