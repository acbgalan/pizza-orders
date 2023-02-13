using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using pizza_orders.data.Models;

namespace pizza_orders.data.Repositories.Interfaces
{
    public interface IPizzaRepository : IRepositoryAsync<Pizza>
    {
        Task<bool> ExitsAsync(string name);
    }
}
