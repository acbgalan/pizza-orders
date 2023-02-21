using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pizza_orders.Requests.Pizza
{
    public class UpdatePizzaRequest
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "Se esperaba una cadena de texto en el campo {0} con un máximo de {1} caracteres")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [StringLength(250, ErrorMessage = "Se esperaba una cadena de texto en el campo {0} con un máximo de {1} caracteres")]
        public string Description { get; set; }

        [JsonPropertyName("prize")]
        public decimal Prize { get; set; }

        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }
}
