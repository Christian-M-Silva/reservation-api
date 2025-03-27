using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;

namespace ReservationApi.Interfaces.IServices
{
    public interface IAuth
    {
        Task<UserEntity> RegisterAsync(RegisterRequest user);
    }
}
