using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;

namespace ReservationApi.Services
{
    public class AuthService(IBaseRepository<UserEntity> baseRepository, IAuthRepository authRepository) : IAuthService
    {
        private readonly IBaseRepository<UserEntity> _baseRepository = baseRepository;
        private readonly IAuthRepository _authRepository = authRepository;

        public async Task<UserEntity?> GetUser(LoginRequest loginRequest)
        {
            try
            {
                UserEntity? userEntity = await _authRepository.GetUserByEmailAsync(loginRequest.Email);
                if (userEntity == null)
                {
                    return userEntity;
                }
                bool isCorrectPassword = EncryptionService.VerifyPassword(loginRequest.Password, userEntity.Password);

                if (!isCorrectPassword) return null;

                return userEntity;
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

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
                return await _baseRepository.InsertAsync(userEntity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
