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

        [DisplayName("Precio unitario")]
        [Precision(9, 2)]
        public decimal UnitPrize { get; set; }

        [DisplayName("Cantidad")]
        public int Quantity { get; set; }

        [DisplayName("Descuento")]
        [Range(0, 100, ErrorMessage = "Se esperaba un valor entre {1} y {2}")]
        public byte Discount { get; set; }

        [DisplayName("Importe")]
        [Precision(9, 2)]
        public decimal Amount { get; set; }
    }
}
