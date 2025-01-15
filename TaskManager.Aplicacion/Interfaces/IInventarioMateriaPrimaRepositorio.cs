using TaskManager.Dominio.Entidades;

namespace TaskManager.Aplicacion.Interfaces
{
    public interface IInventarioMateriaPrimaRepositorio
    {
        Task AñadirAlInventarioAsync(int compraId, InventarioMateriaPrimaModel inventarioModel);
        Task<MemoryStream> ObtenerInventarioMateriaPrimaCSV();
        Task<MemoryStream> ObtenerInventarioMateriaPrimaExcel();
        Task<IEnumerable<InventarioMateriaPrimaModel>> ObtenerListadoInventarioMateriaPrimaAsync();
    }
}
