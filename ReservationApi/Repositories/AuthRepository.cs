using Microsoft.EntityFrameworkCore;
using ReservationApi.Data;
using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Shared;

namespace ReservationApi.Repositories
{
    public class AuthRepository(MyDbContext dbContext) : IAuthRepository
    {
        private readonly MyDbContext _dbContext = dbContext;
        public async Task<RefreshTokenModel> GenerateRefrsehToken(string email)
        {
            try
            {
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<UserEntity?> GetUserByEmailAsync(string email)
        {
            try
            {
                return await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
