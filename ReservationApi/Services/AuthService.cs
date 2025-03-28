using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;

namespace ReservationApi.Services
{
    public class AuthService(IAuthRepository authRepository) : IAuthService
    {
        readonly IAuthRepository _authRepository = authRepository;
        public async Task<UserEntity> RegisterAsync(RegisterUserRequest user)
        {
            try
            {
                string hashPassword = EncryptionService.HashPassword(user.Password);
                user.Password = hashPassword;
                return await _authRepository.RegisterAsync(user);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
