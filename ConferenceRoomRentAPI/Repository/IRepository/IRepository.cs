﻿using System.Linq.Expressions;

namespace ConferenceRoomRentAPI.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<T> GetAsync(Expression<Func<T, bool>> expression, string includeProperties = null);
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T,bool>> expression=null, string includeProperties = null, int pageSize=3, int pageNumber=1);
        Task CreateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
