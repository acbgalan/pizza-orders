using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pizza_orders.data.Models
{
    [Table("Clients")]
    public class Client
    {
        public int Id { get; set; }

        [DisplayName("Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "Se esperaba una cadena de texto con un máximo de {1} caracteres")]
        public string Name { get; set; }

        [DisplayName("Dirección")]
        [StringLength(250, ErrorMessage = "Se esperaba una cadena de texto con un máximo de {1} caracteres")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "El campo {0} tiene un formato no válido")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(250, ErrorMessage = "Se esperaba una cadena de texto con un máximo de {1} caracteres")]
        [EmailAddress(ErrorMessage = "Formato no válido")]
        public string Email { get; set; }

        public List<Order> Orders { get; set; }
    }
}
