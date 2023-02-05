using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza_orders.data.Models
{
    [Table("Ingredients")]
    public class Ingredient
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "Se esperaba una cadena de texto en el campo {0} con un máximo de {1} caracteres")]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        public List<Pizza> Pizzas { get; set; }
    }
}
