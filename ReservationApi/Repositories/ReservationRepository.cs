using Microsoft.EntityFrameworkCore;
using ReservationApi.Data;
using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;

namespace ReservationApi.Repositories
{
    public class ReservationRepository(MyDbContext dbContext) : IReservationRepository
    {
        private readonly MyDbContext _dbContext = dbContext;

        public async Task<IEnumerable<ReservationEntity?>> GetReservations(FilterRequest filterRequest)
        {
            try
            {
                var query = _dbContext.Reservations.Where(coluna => coluna.ClientId == filterRequest.IdClient);

                if (filterRequest.CheckIn.HasValue)
                {
                    query = query.Where(coluna => coluna.CheckIn == filterRequest.CheckIn);
                }
                if (filterRequest.RoomNumber.HasValue)
                {
                    query = query.Where(coluna => coluna.RoomNumber == filterRequest.RoomNumber);
                }

                return await query.OrderBy(coluna => coluna.CheckIn).ToListAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
