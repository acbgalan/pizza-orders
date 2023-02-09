using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizza_orders.data.Models
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        public int Id { get; set; }

        [ForeignKey("Pizza")]
        public int PizzaId { get; set; }

        [DisplayName("Cantidad")]
        public int Amount { get; set; }

        public Pizza Pizza { get; set; }
    }
}
