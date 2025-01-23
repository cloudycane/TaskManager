using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Aplicacion.Interfaces;
using TaskManager.Aplicacion.Servicios;
using TaskManager.Infraestructura.Data;
using TaskManager.Infraestructura.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
// Dependency Injection 

builder.Services.AddScoped<ReservacionService>();
builder.Services.AddScoped<ISuministradorService, SuministradorService>();
builder.Services.AddScoped<ProductoSuministradorService>();
builder.Services.AddScoped<ComprarProductoSuministradorService>();
builder.Services.AddScoped<IPedidosClienteService, PedidosClienteService>();
builder.Services.AddScoped<IOrdenAdquisicionService, OrdenAdquisicionService>();

builder.Services.AddScoped<IReservacionRepositorio, ReservacionRepositorio>(); 
builder.Services.AddScoped<ISuministradorRepositorio, SuministradorRepositorio>();
builder.Services.AddScoped<IProductoSuministradorRepositorio, ProductoSuministradorRepositorio>();
builder.Services.AddScoped<IOrdenarProductoSuministradorRepositorio, OrdenarProductoSuministradorRepositorio>();
builder.Services.AddScoped<ICompraProductoSuministradorFacturacionRepositorio, CompraProductoSuministradorFacturacionRepositorio>();
builder.Services.AddScoped<IInventarioMateriaPrimaRepositorio, InventarioMateriaPrimaRepositorio>();
builder.Services.AddScoped<IIngrendientesRepositorio, IngrendientesRepositorio>();
builder.Services.AddScoped<IProductosParaVenderRepositorio, ProductosParaVenderRepositorio>();
builder.Services.AddScoped<IPedidosClienteRepositorio, PedidosClienteRepositorio>();
builder.Services.AddScoped<IUnitofWork, UnitofWork>();

var app = builder.Build();

// Rotativa

Rotativa.AspNetCore.RotativaConfiguration.Setup("wwwroot", wkhtmltopdfRelativePath: "Rotativa");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{area=Cliente}/{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
