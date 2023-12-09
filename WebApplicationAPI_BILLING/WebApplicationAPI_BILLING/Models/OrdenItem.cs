using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApplicationAPI_BILLING.Models;

namespace WebApplicationAPI_BILLING.Models
{
    public class OrdenItem
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int OrdenId { get; set; }
        [Required]
        public int ProductoId { get; set; }
        [Required]
        public decimal UnitPrecio { get; set; } = 0;
        [Required]
        public int Cantidad { get; set; } = 1;
        [ForeignKey("OrdenId")]
        public OrdenC? OrdenC { get; set; }
        [ForeignKey("ProductoId")]
        public Producto? Producto { get; set; }
        public decimal? Subtotal { get; set; } = 0;
    }
}