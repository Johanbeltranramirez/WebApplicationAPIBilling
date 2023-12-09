
﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplicationAPI_BILLING.Models
{
    public class OrdenC
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTime FechaOrd { get; set; } = DateTime.Now;
        //[MaxLength(128)]
        public Guid NumeroOrd { get; set; } = Guid.NewGuid(); // Establece un nuevo GUID por defecto

        [Required]
        public int ClienteId { get; set; }
        public decimal PagoTotal { get; set; } = 0;

        [ForeignKey("ClienteId")]
        public Cliente? Cliente { get; set; }
        public virtual ICollection<OrdenItem> OrdenItems { get; set; }
    }
}