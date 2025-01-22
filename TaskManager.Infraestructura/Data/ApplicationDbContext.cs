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
        public virtual DbSet<PedidoProductoModel> PedidoProducto { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PedidosClienteModel>()
            .Ignore(e => e.ProductosParaVenderIds)
            .Ignore(i => i.CantidadPedido);

            modelBuilder.Entity<PedidoProductoModel>()
            .HasKey(pp => new { pp.PedidoClienteId, pp.ProductoParaVenderId });

            modelBuilder.Entity<PedidoProductoModel>()
                .HasOne(pp => pp.PedidoCliente)
                .WithMany(pc => pc.PedidoProductos)
                .HasForeignKey(pp => pp.PedidoClienteId);

            modelBuilder.Entity<PedidoProductoModel>()
                .HasOne(pp => pp.ProductoParaVender)
                .WithMany(pv => pv.PedidoProductos)
                .HasForeignKey(pp => pp.ProductoParaVenderId);

            base.OnModelCreating(modelBuilder);


        }
    }
}
