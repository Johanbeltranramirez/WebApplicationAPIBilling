using System.ComponentModel.DataAnnotations;

namespace WebApplicationAPI_BILLING.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string PrimerNombre { get; set; }

        [MaxLength(64)]
        public string Apellido { get; set; } = string.Empty;

        [MaxLength(128)]
        public string Ciudad { get; set; } = string.Empty;

        [MaxLength(128)]
        public string Pais { get; set; }

        [MaxLength(16)]
        public string Telefono { get; set; }

        public ICollection<OrdenC>? OrdenesC { get; set; }
    }
}