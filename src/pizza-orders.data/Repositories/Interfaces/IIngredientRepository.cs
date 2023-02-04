using pizza_orders.data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizza_orders.data.Repositories.Interfaces
{
    public interface IIngredientRepository : IRepositoryAsync<Ingredient>
    {
    }
}
