using System.ComponentModel.DataAnnotations;

namespace FinanzAppApi.Models
{
    public class Cuenta
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UsuarioId { get; set; }

        [Required, MaxLength(50)]
        public string Nombre { get; set; } // Ej: "Cuenta de Ahorros", "Efectivo"

        [Required, MaxLength(20)]
        public string Tipo { get; set; } // "Banco", "Efectivo", "Tarjeta de Crédito"

        [Required]
        public decimal Saldo { get; set; } = 0;

        public Usuario Usuario { get; set; } // Relación con el usuario
    }
}
