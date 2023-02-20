using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace pizza_orders.Responses.Orders
{
    public class OrderResponseDetail
    {
        [JsonPropertyName("pizzaId")]
        public int PizzaId { get; set; }

        [JsonPropertyName("product")]
        public string Product { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        [JsonPropertyName("unitPrize")]
        public decimal UnitPrize { get; set; }

        [JsonPropertyName("discount")]
        public byte Discount { get; set; }

        [JsonPropertyName("amount")]
        [Precision(9, 2)]
        public decimal Amount { get; set; }
    }
}
