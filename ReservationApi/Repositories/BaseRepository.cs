using Microsoft.EntityFrameworkCore;
using ReservationApi.Data;
using ReservationApi.Interfaces.IRepositories;
using ReservationApi.Models.Entities;

namespace ReservationApi.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly MyDbContext _context;

        private readonly DbSet<T> _database;

        public BaseRepository(MyDbContext context)
        {
            _context = context;
            _database = _context.Set<T>();
        }

        public async Task<T> InsertAsync(T entity)
        {
            try
            {
                await _database.AddAsync(entity);
                await _context.SaveChangesAsync();

                return entity;
            }
            catch (Exception err)
            {

                throw new Exception(err.Message);
            }
        }
    }

}
