using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;

namespace ReservationApi.Services
{
    public class ReservationService(IBaseRepository<ReservationEntity> reservationRepository) : IResevationService
    {
        private readonly IBaseRepository<ReservationEntity> _reservationRepository = reservationRepository;

        public async Task<ReservationEntity> CreateReservation(ReservationEntity reservation)
        {
            try
            {
                return await _reservationRepository.InsertAsync(reservation);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
