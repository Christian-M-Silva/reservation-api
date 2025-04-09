using ReservationApi.Models.Entities;

namespace ReservationApi.Interfaces.IServices
{
    public interface ILogService
    {
        public Task RegisterLog(LogEntity log);
    }
}
