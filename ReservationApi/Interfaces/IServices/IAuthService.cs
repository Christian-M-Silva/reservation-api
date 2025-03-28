using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;

namespace ReservationApi.Interfaces.IServices
{
    public interface IAuthService
    {
        Task<UserEntity> RegisterAsync(RegisterUserRequest user);
    }
}
