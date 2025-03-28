using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;

namespace ReservationApi.Interfaces.IRepositories
{
    public interface IAuthRepository
    {
        Task<UserEntity> RegisterAsync(RegisterUserRequest user);
    }
}
