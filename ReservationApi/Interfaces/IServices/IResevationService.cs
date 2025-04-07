using ReservationApi.Models.Entities;

namespace ReservationApi.Interfaces.IServices
{
    public interface IResevationService
    {
        public Task<ReservationEntity> CreateReservation(ReservationEntity reservation);
    }
}
