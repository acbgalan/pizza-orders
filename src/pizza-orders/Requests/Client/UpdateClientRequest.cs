using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace pizza_orders.Requests.Client
{
    public class UpdateClientRequest
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "Se esperaba una cadena de texto con un máximo de {1} caracteres")]
        public string Name { get; set; }

        [StringLength(250, ErrorMessage = "Se esperaba una cadena de texto con un máximo de {1} caracteres")]
        public string Address { get; set; }

        [Phone(ErrorMessage = "El campo {0} tiene un formato no válido")]
        public string Phone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo {0} es requerido")]
        [StringLength(250, ErrorMessage = "Se esperaba una cadena de texto con un máximo de {1} caracteres")]
        [EmailAddress(ErrorMessage = "Formato no válido")]
        public string Email { get; set; }
    }
}
