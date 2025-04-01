using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Shared;

namespace ReservationApi.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        public Task<RefreshTokenModel> GenerateRefrsehToken(string email)
        {
            throw new NotImplementedException();
        }

        public Task<UserEntity> GetUserByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
}
