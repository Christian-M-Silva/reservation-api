using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;

namespace ReservationApi.Services
{
    public class AuthService(IBaseRepository<UserEntity> authRepository) : IAuthService
    {
        private readonly IBaseRepository<UserEntity> _authRepository = authRepository;
        public async Task<UserEntity> RegisterAsync(RegisterUserRequest user)
        {
            try
            {
                string hashPassword = EncryptionService.HashPassword(user.Password);
                UserEntity userEntity = new()
                {
                    Role = user.Role,
                    Password = hashPassword,
                    Email = user.Email,
                    Name = user.Name
                };
                return await _authRepository.InsertAsync(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
