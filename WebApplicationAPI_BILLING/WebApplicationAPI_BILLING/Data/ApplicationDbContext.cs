using Microsoft.EntityFrameworkCore;

namespace WebApplicationAPI_BILLING.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<Models.Proveedor> Proveedores { get; set; }
        public DbSet<Models.Cliente> Clientes { get; set; }
        public DbSet<Models.OrdenC> OrdenesC { get; set; }
        public DbSet<Models.OrdenItem> OrdenItems { get; set; }
        public DbSet<Models.Producto> Productos { get; set; }

    }
}
