using CleanNow.Core.Application.Interfaces.Repositories;
using CleanNow.Infrastructured.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Infrastructured.Persistence.Repository
{
    public class GeneryRepository<T>: IGenericRepository<T>  where T:class
    {
        private readonly ApplicationContext _context;
        public GeneryRepository(ApplicationContext context)
        {
            _context = context;
        }
        public virtual async Task<T> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<ICollection<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public virtual async Task<ICollection<T>> GetAllIncludeAsync(List<string> properties)
        {
            var query = _context.Set<T>().AsQueryable();
            foreach (var item in properties)
            {
                query = query.Include(item);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public virtual async Task UpdateAsync(T entity, int id)
        {
            T entityToUpdate = await _context.Set<T>().FindAsync(id);
            _context.Entry(entityToUpdate).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
        }
    }
}
