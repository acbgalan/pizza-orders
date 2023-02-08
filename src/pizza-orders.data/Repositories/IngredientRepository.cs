using Microsoft.EntityFrameworkCore;
using pizza_orders.data.Models;
using pizza_orders.data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace pizza_orders.data.Repositories
{
    public class IngredientRepository : IIngredientRepository
    {
        private readonly ApplicationDbContext _context;

        #region Async
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

        public async Task<bool> ExitsAsync(List<int> ids)
        {
            var ingredientIds = await _context.Ingredients.Where(x => ids.Contains(x.Id)).Select(x => x.Id).ToListAsync();
            return ingredientIds.Count != ids.Count ? false : true;
        }

        #endregion

        #region Sync
        public void Add(Ingredient entity)
        {
            _context.Ingredients.Add(entity);
        }

        public List<Ingredient> GetAll()
        {
            return _context.Ingredients.ToList();
        }

        public Ingredient Get(int id)
        {
            return _context.Ingredients.Find(id);
        }

        public void Update(Ingredient entity)
        {
            _context.Entry<Ingredient>(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var ingredient = this.Get(id);

            if (ingredient != null)
            {
                _context.Entry<Ingredient>(ingredient).State entityState = EntityState.Deleted;
            }
        }

        public void Delete(Ingredient entity)
        {
            _context.Entry<Ingredient>(entity).State = EntityState.Deleted;
        }

        public bool Exits(int id)
        {
            return _context.Ingredients.Any(x => x.Id == id);
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        #endregion

    }
}
