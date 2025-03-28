using ReservationApi.Models.Entities;

namespace ReservationApi.Interfaces.IRepositories
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task<T> InsertAsync(T entity);
    }
}
