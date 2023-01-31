using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizza_orders.data.Repositories.Interfaces
{
    public interface IRepositoryAsync<T>
    {
        Task AddAsync(T entity);
        Task<T> GetAsync(int id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task DeleteAsync(T entity);
        Task<bool> ExitsAsync(int id);
        Task<int> SaveASync();
    }
}
