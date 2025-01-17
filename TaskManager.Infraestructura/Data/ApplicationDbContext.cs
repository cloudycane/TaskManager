using Microsoft.EntityFrameworkCore;
using TaskManager.Dominio.Entidades;

namespace TaskManager.Infraestructura.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<ReservacionModel> Reservaciones { get; set; }
        public virtual DbSet<SuministradorModel> Suministradores { get; set; }
        public virtual DbSet<ProductoSuministradorModel> ProductosSuministradores { get; set; }
        public virtual DbSet<OrdenAdquisicionModel> OrdenesAdquisicion { get; set; }
        public virtual DbSet<CompraProductoSuministradorModel> CompraProductoSuministradorFacturacion { get; set; }
        public virtual DbSet<InventarioMateriaPrimaModel> InventarioMateriaPrimas { get; set; }
        public virtual DbSet<ProductosParaVenderModel> ProductosParaVender { get; set; }
        public virtual DbSet<IngrendientesModel> Ingrendientes { get; set; }
        public virtual DbSet<PedidosClienteModel> PedidosClientes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


        }
    }
}
