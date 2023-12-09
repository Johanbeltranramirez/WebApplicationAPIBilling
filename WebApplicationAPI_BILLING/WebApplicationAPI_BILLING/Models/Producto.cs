
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationAPI_BILLING.Models
{
    public class Producto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(128)]
        public string NombreProducto { get; set; }
        [Required]
        public int ProveedorId { get; set; }
        public decimal UnitPrecio { get; set; } = 0;
        [MaxLength(64)]
        public string Paquete { get; set; }
        public bool EsDescontinuado { get; set; } = false;
        [ForeignKey("ProveedorId ")]
        public Proveedor? Proveedor { get; set; }
        public ICollection<OrdenItem>? OrdenItems { get; set; }
    }
}