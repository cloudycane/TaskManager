using Microsoft.EntityFrameworkCore;
using TaskManager.Dominio.Entidades;

namespace TaskManager.Infraestructura.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ReservacionModel> Reservaciones { get; set; }
        public DbSet<SuministradorModel> Suministradores { get; set; }
        public DbSet<ProductoSuministradorModel> ProductosSuministradores { get; set; }
        public DbSet<OrdenAdquisicionModel> OrdenesAdquisicion { get; set; }
        public DbSet<CompraProductoSuministradorModel> CompraProductoSuministradorFacturacion { get; set; }
        public DbSet<InventarioMateriaPrimaModel> InventarioMateriaPrimas { get; set; }
        public DbSet<ProductosParaVenderModel> ProductosParaVender { get; set; }
        public DbSet<IngrendientesModel> Ingrendientes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
