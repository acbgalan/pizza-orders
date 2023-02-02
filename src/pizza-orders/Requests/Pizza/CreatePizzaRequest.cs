using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace pizza_orders.Requests.Pizza
{
    public class CreatePizzaRequest
    {
        [JsonPropertyName("name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(5, ErrorMessage = "Se esperaba una cadena de texto en el campo {0} con un máximo de {1} caracteres")]
        public string Name { get; set; }

        [JsonPropertyName("description")]
        [StringLength(250, ErrorMessage = "Se esperaba una cadena de texto en el campo {0} con un máximo de {1} caracteres")]
        public string Description { get; set; }

        [JsonPropertyName("prize")]
        [Precision(9, 2)]
        public decimal Prize { get; set; }

        [JsonPropertyName("available")]
        public bool Available { get; set; }
    }
}