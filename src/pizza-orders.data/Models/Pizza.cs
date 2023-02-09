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

        [DisplayName("Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "Se esperaba una cadena de texto con un máximo de {1} caracteres")]        
        public string Name { get; set; }

        [DisplayName("Descripción")]
        [StringLength(250, ErrorMessage = "Se esperaba una cadena de texto con un máximo de {1} caracteres")]
        public string Description { get; set; }

        [DisplayName("Precio")]
        [Precision(9, 2)]        
        public decimal Prize { get; set; }

        [DisplayName("Disponible")]
        public bool Available { get; set; }

        public List<Ingredient> Ingredients { get; set; }
    }
}