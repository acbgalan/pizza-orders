using Microsoft.EntityFrameworkCore;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizza_orders.data.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            return await _context.Orders.Include(x => x.OrderDetails).ToListAsync();
        }

        public async Task<Order> GetAsync(int id)
        {
            return await _context.Orders.Include(x => x.OrderDetails).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Order entity)
        {
            await Task.Run(() =>
            {
                _context.Entry<Order>(entity).State = EntityState.Modified;
            });
        }

        public async Task DeleteAsync(int id)
        {
            Order order = await this.GetAsync(id);

            if (order != null)
            {
                _context.Entry<Order>(order).State = EntityState.Deleted;
            }
        }

        public async Task DeleteAsync(Order entity)
        {
            await Task.Run(() =>
            {
                _context.Entry(entity).State = EntityState.Modified;
            });
        }

        public async Task<bool> ExitsAsync(int id)
        {
            return await _context.Orders.AnyAsync(x => x.Id == id);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
