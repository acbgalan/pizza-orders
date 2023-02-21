using Microsoft.EntityFrameworkCore;
using pizza_orders.data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace pizza_orders.Responses.OrderDetail
{
    public class OrderDetailResponse
    {
        public int OrderId { get; set; }
        public int PizzaId { get; set; }

        [Precision(9, 2)]
        public decimal UnitPrize { get; set; }

        public int Quantity { get; set; }

        public byte Discount { get; set; }
        
        public decimal Amount { get; set; }
    }
}
