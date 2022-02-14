using DatabaseAccess.Entities;
using DatabaseAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAccess
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly CarWorldContext _context;
        internal DbSet<T> dbSet;
        public Repository(CarWorldContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public async Task Add(T entity)
        {
           await dbSet.AddAsync(entity);
        }
        public async Task AddRange(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }
        public async Task<T> Get(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, bool>> filter = null, 
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
                                     string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new string[] { ", " },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            return await query.ToListAsync();
        }

        public async Task<T> GetFirstOrDefault(Expression<Func<T, bool>> filter = null, 
                                   string includeProperties = null)
        {
            IQueryable<T> query = dbSet;

            if (filter != null)
            {
                query = query.AsNoTracking().Where(filter);
            }

            if (includeProperties != null)
            {
                foreach (var includeProp in includeProperties.Split(new string[] { ", " },
                    StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProp);
                }
            }
            return await query.FirstOrDefaultAsync();
        }

        public async Task Remove(int id)
        {
            T entity = await dbSet.FindAsync(id);
            Remove(entity);
        }

        //public async Task<T> GetAsNoTracking(Expression<Func<T, bool>> filter)
        //{
        //    return await dbSet.AsNoTracking().Where(filter).FirstOrDefaultAsync();
        //}


        public void Remove(T entity)
        {
            dbSet.Remove(entity);
            
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
            
        }

        public void Update(T entity)
        {
            dbSet.Update(entity); 
        }

        public void UpdateRange(IEnumerable<T> entity)
        {
            dbSet.UpdateRange(entity);
        }
    }
}
