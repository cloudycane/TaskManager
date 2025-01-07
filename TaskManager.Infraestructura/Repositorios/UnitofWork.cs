using TaskManager.Infraestructura.Data;

namespace TaskManager.Infraestructura.Repositorios
{
    public interface IUnitofWork
    {
        Task GuardarAsync();
    }
    public class UnitofWork : IUnitofWork
    {
        private readonly ApplicationDbContext _context;

        public UnitofWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // GUARDAR 

        public async Task GuardarAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
