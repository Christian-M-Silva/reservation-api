using ReservationApi.Models.Entities;
using ReservationApi.Models.Request;

namespace ReservationApi.Interfaces.IRepositories
{
    public interface IReservationRepository
    {
        public Task<IEnumerable<ReservationEntity?>> GetReservations(FilterRequest? filterRequest);
    }
}
