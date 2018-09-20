using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.DataAccess.Repositories
{
    public interface IRepository<T> where T : class
    {
        void AddAsync(T entity);
        void Update(T entity);
        void Delete(int id);
        void DeleteMany(Expression<Func<T, bool>> condition);
        Task<IEnumerable<T>> GetAllAsync();
        Task<PagedList<T>> GetAsync(CommonParams param);
        Task<T> GetByIdAsync(int id);
        Task<T> GetIncludingAsync(Expression<Func<T, bool>> conditions, params Expression<Func<T, object>>[] properties);
    }
}