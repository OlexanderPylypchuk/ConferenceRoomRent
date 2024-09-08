using System.Linq.Expressions;
using ConferenceRoomRentAPI.Data;
using ConferenceRoomRentAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ConferenceRoomRentAPI.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _db;
        private readonly DbSet<T> _dbSet;
        public Repository(AppDbContext appDbContext)
        {
            _db= appDbContext;
            _dbSet= _db.Set<T>();
        }
        public async Task CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, int pageSize = 3, int pageNumber = 1)
        {
            IQueryable<T> query = _dbSet;
            if (expression != null)
            {
                query = query.Where(expression);
            }
            //pagination
            if (pageSize > 0)
            {
                if(pageSize > 100)
                {
                    pageSize = 100;
                }
                query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
            }
            return query;
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> expression)
        {
            IQueryable<T> query = _dbSet;
            if(expression != null)
            {
                query=query.Where(expression);
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
