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

        public Task DeleteRefrsehToken(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<RefreshTokenModel?> GenerateRefrsehToken(string email)
        {
            try
            {
                UserEntity? user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Email == email);

                if (user == null) return null;

                user.RefreshToken = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                user.ExpirationDateRefreshToken = new DateOnly().AddDays(7);

                _dbContext.Entry(user).Property(u => u.RefreshToken).IsModified = true;
                _dbContext.Entry(user).Property(u => u.ExpirationDateRefreshToken).IsModified = true;
                await _dbContext.SaveChangesAsync();

                return new RefreshTokenModel() { ExpirationDateRefreshToken = user.ExpirationDateRefreshToken, RefreshToken = user.RefreshToken};
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
