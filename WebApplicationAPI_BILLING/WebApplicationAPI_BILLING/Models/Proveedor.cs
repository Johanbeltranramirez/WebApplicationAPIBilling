
﻿using System.ComponentModel.DataAnnotations;

namespace WebApplicationAPI_BILLING.Models
{
    public class Proveedor
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string NombreCompania { get; set; }
        [MaxLength(64)]
        public string NombreContacto { get; set; }
        [MaxLength(64)]
        public string TituloContacto { get; set; }
        [MaxLength(64)]
        public string Ciudad { get; set; }
        [MaxLength(64)]
        public string Pais { get; set; }
        [MaxLength(16)]
        public string Telefono { get; set; }
        [EmailAddress(ErrorMessage = "El valor introducido no es una dirección de correo electrónico válida.")]
        public string? Email { get; set; }
        public ICollection<Producto>? Productos { get; set; }
    }
}