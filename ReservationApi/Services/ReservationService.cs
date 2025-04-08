using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;
using ReservationApi.Repositories;

namespace ReservationApi.Services
{
    public class ReservationService(IBaseRepository<ReservationEntity> baseRepository, IReservationRepository reservationRepository) : IResevationService
    {
        private readonly IBaseRepository<ReservationEntity> _baseRepository = baseRepository;
        private readonly IReservationRepository _reservationRepository = reservationRepository;


        public async Task<ReservationEntity> CreateReservation(ReservationEntity reservation)
        {
            try
            {
                return await _baseRepository.InsertAsync(reservation);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<IEnumerable<ReservationEntity?>> GetReservations(FilterRequest filterRequest)
        {
            try
            {
                return await _reservationRepository.GetReservations(filterRequest);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
