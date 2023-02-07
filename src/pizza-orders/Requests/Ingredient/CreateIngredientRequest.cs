using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace pizza_orders.Requests.Ingredient
{
    public class CreateIngredientRequest
    {
        [JsonPropertyName("name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "Se esperaba una cadena de texto en el campo {0} con un máximo de {1} caracteres")]
        public string Name { get; set; }
    }
}
