using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NGH.API.Helpers;
using NGH.API.Models;

namespace NGH.API.DataAccess.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly NGHDataContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(NGHDataContext context)
        {
            this._context = context;
            this._dbSet = this._context.Set<T>();
        }
        public async void AddAsync(T entity)
        {
            await this._dbSet.AddAsync(entity);
        }
        public void Update(T entity)
        {
            this._dbSet.Attach(entity);
            this._context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var obj = this._dbSet.Find(id);
            if (obj != null)
                this._dbSet.Remove(obj);
        }

        public void DeleteMany(Expression<Func<T, bool>> condition)
        {
            IEnumerable<T> objects = this._dbSet.Where(condition);
            foreach (T obj in objects) 
            {
                this._dbSet.Remove(obj);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await this._dbSet.ToListAsync();
        }
        public async Task<PagedList<T>> GetAsync(CommonParams param)
        {
            var res = this._dbSet.AsQueryable();

            return await PagedList<T>.CreateListAsync(res, param.PageNumber, param.PageSize);
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await this._dbSet.FindAsync(id);  
        }
        public async Task<T> GetIncludingAsync(Expression<Func<T, bool>> conditions, params Expression<Func<T, object>>[] properties)
        {
            IQueryable<T> ret = this._dbSet.Where(conditions);
            if (properties != null)
            {
                foreach(var prop in properties) {
                    ret = ret.Include(prop);
                }
            }
            return await ret.AsNoTracking().FirstOrDefaultAsync();
        }
    }
}