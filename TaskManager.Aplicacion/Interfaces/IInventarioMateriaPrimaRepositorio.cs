using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IInventarioMateriaPrimaRepositorio
    {
        Task AñadirAlInventarioAsync(int compraId, InventarioMateriaPrimaModel inventarioModel);
        Task<IEnumerable<InventarioMateriaPrimaModel>> ObtenerListadoInventarioMateriaPrimaAsync();
    }
}
