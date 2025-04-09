using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Interfaces.IServices;
using ReservationApi.Models.Entities;

namespace ReservationApi.Services
{
    public class LogService(IBaseRepository<LogEntity> baseRepository) : ILogService
    {
        private readonly IBaseRepository<LogEntity> _baseRepository = baseRepository;

        public async Task RegisterLog(LogEntity log)
        {
            await _baseRepository.InsertAsync(log);
        }
    }
}
