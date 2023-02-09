using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pizza_orders.data.Models
{
    public enum PaymentMethod { Cash, Card, Prepaid }

    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }

        [DisplayName("Fecha")]
        public DateTime Date { get; set; }

        [DisplayName("Método de pago")]
        public PaymentMethod PaymentMethod { get; set; }

        [DisplayName("Precio")]
        [Precision(9, 2)]        
        public decimal Prize { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public Client Client { get; set; }
    }
}
