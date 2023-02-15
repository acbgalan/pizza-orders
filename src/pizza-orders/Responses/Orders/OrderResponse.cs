using Microsoft.EntityFrameworkCore;
using pizza_orders.data.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;
using pizza_orders.Responses.OrderDetail;

namespace pizza_orders.Responses.Orders
{
    public class OrderResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("paymentmethod")]
        [DisplayName("Método de pago")]
        public PaymentMethod PaymentMethod { get; set; }

        [JsonPropertyName("prize")]
        public decimal Prize { get; set; }

        [JsonPropertyName("clientid")]
        public int ClientId { get; set; }

        [JsonPropertyName("state")]
        public State State { get; set; }

        public List<OrderDetailResponse> OrderDetails { get; set; }


    }
}
