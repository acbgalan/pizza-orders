using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pizza_orders.data.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
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

        public Order Order { get; set; }
        public Pizza Pizza { get; set; }
    }
}
