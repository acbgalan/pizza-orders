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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(OrderDetail entity)
        {
            await _context.OrderDetails.AddAsync(entity);
        }

        public async Task<List<OrderDetail>> GetAllAsync()
        {
            return await _context.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail> GetAsync(int id)
        {
            return await _context.OrderDetails.FindAsync(id);
        }

        public async Task UpdateAsync(OrderDetail entity)
        {
            await Task.Run(() =>
            {
                _context.OrderDetails.Update(entity);
            });
        }

        public async Task DeleteAsync(int id)
        {
            var orderDetail = await this.GetAsync(id);

            if (orderDetail != null)
            {
                _context.Entry<OrderDetail>(orderDetail).State = EntityState.Deleted;
            }
        }

        public async Task DeleteAsync(OrderDetail entity)
        {
            await Task.Run(() =>
            {
                _context.Entry<OrderDetail>(entity).State = EntityState.Deleted;
            });
        }

        //TODO: Se debería redefinir para que ajuste para 2 PK
        public Task<bool> ExitsAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<List<OrderDetail>> FillPrices(List<OrderDetail> orderDetails)
        {
            foreach (var item in orderDetails)
            {
                decimal pizzaPrize = await (from p in _context.Pizzas
                                            where p.Id == item.PizzaId && p.Available == true
                                            select p.Prize).FirstOrDefaultAsync();

                if (pizzaPrize != 0)
                {
                    item.UnitPrize = pizzaPrize;
                    item.Amount = pizzaPrize * item.Quantity;
                }
            }

            return orderDetails;
        }
    }
}
