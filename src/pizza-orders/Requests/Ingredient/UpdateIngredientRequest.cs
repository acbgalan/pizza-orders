using System.ComponentModel.DataAnnotations;

namespace pizza_orders.Requests.Ingredient
{
    public class UpdateIngredientRequest
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "Se esperaba una cadena de texto en el campo {0} con un máximo de {1} caracteres")]
        public string Name { get; set; }
    }
}
