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

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("address")]
        public string Address { get; set; }

        [JsonPropertyName("Phone")]
        public string Phone { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("paymentmethod")]
        public string PaymentMethod { get; set; }

        [JsonPropertyName("prize")]
        public decimal Prize { get; set; }

        [JsonPropertyName("state")]
        public string State { get; set; }

        //public List<OrderDetailResponse> OrderDetails { get; set; }


    }
}
