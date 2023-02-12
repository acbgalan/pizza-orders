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
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Client entity)
        {
            await _context.Clients.AddAsync(entity);
        }

        public async Task<List<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client> GetAsync(int id)
        {
            return await _context.Clients.FindAsync(id);
        }

        public async Task UpdateAsync(Client entity)
        {
            await Task.Run(() =>
            {
                _context.Entry<Client>(entity).State = EntityState.Modified;
            });
        }

        public async Task DeleteAsync(int id)
        {
            Client client = await this.GetAsync(id);

            if (client != null)
            {
                _context.Entry<Client>(client).State = EntityState.Deleted;
            }
        }

        public async Task DeleteAsync(Client entity)
        {
            await Task.Run(() =>
            {
                _context.Entry<Client>(entity).State = EntityState.Deleted;
            });
        }

        public async Task<bool> ExitsAsync(int id)
        {
            return await _context.Clients.AnyAsync(x => x.Id == id);

        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
