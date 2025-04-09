using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;

namespace ReservationApi.Interfaces.IServices
{
    public interface IResevationService
    {
        public Task<ReservationEntity> CreateReservation(ReservationEntity reservation);
        public Task<IEnumerable<ReservationEntity?>> GetReservations(FilterRequest? filterRequest);
        public Task DeleteReservation(Guid id);
    }
}
