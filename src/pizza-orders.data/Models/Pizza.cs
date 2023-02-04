using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;

namespace pizza_orders.data.Models
{
    [Table("Pizzas")]
    public class Pizza
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "Se esperaba una cadena de texto en el campo {0} con un máximo de {1} caracteres")]
        [DisplayName("Nombre")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Se esperaba una cadena de texto en el campo {0} con un máximo de {1} caracteres")]
        [DisplayName("Descripción")]
        public string Description { get; set; }

        [Precision(9, 2)]
        [DisplayName("Precio")]
        public decimal Prize { get; set; }

        [DisplayName("Disponible")]
        public bool Available { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}