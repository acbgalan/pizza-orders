using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pizza_orders.Responses.Ingredient
{
    public class IngredientResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
