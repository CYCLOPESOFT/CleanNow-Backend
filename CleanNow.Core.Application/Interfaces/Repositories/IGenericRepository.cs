using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanNow.Core.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T> where T : class
    {
        Task<ICollection<T>> GetAllAsync ();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity, int id);
        Task<T> DeleteAsync(T entity);
        Task<T> GetAsync(int id);
        Task<ICollection<T>> GetAllIncludeAsync(List<string> properties);

    }
}
