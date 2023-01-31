using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;

namespace pizza_orders.data.Repositories
{
    public class PizzaRepository : IPizzaRepository
    {
        private readonly ApplicationDbContext _context;

        public PizzaRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Pizza entity)
        {
            await _context.Pizzas.AddAsync(entity);
        }

        public async Task<List<Pizza>> GetAllAsync()
        {
            return await _context.Pizzas.ToListAsync();
        }

        public async Task<Pizza> GetAsync(int id)
        {
            return await _context.Pizzas.FindAsync(id);
        }

        public async Task UpdateAsync(Pizza entity)
        {
            await Task.Run(() =>
            {
                _context.Entry<Pizza>(entity).State = EntityState.Modified;
            });
        }

        public async Task DeleteAsync(int id)
        {
            var pizza = await this.GetAsync(id);

            if (pizza != null)
            {
                _context.Entry<Pizza>(pizza).State = EntityState.Deleted;
            }
        }

        public async Task DeleteAsync(Pizza entity)
        {
            await Task.Run(() =>
            {
                _context.Entry<Pizza>(entity).State = EntityState.Deleted;
            });
        }

        public async Task<bool> ExitsAsync(int id)
        {
            return await _context.Pizzas.AnyAsync(x => x.Id == id);
        }

        public async Task<int> SaveASync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
