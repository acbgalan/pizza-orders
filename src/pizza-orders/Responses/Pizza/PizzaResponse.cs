using System.Text.Json.Serialization;

namespace pizza_orders.Responses.Pizza
{
    public class PizzaResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("prize")]
        public decimal Prize { get; set; }

        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }
}
