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
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        public IngredientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Ingredient entity)
        {
            await _context.Ingredients.AddAsync(entity);
        }

        public async Task<List<Ingredient>> GetAllAsync()
        {
            return await _context.Ingredients.ToListAsync();
        }

        public async Task<Ingredient> GetAsync(int id)
        {
            return await _context.Ingredients.FindAsync(id);
        }

        public async Task UpdateAsync(Ingredient entity)
        {
            await Task.Run(() =>
            {
                _context.Entry<Ingredient>(entity).State = EntityState.Modified;
            });
        }

        public async Task DeleteAsync(int id)
        {
            var ingredient = await this.GetAsync(id);

            if (ingredient != null)
            {
                _context.Entry<Ingredient>(ingredient).State = EntityState.Deleted;
            }
        }

        public async Task DeleteAsync(Ingredient entity)
        {
            await Task.Run(() =>
            {
                _context.Entry<Ingredient>(entity).State = EntityState.Deleted;
            });
        }

        public async Task<bool> ExitsAsync(int id)
        {
            return await _context.Ingredients.AnyAsync(x => x.Id == id);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

    }
}
