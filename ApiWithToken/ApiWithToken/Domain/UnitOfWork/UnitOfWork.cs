using System.Threading.Tasks;
using ApiWithToken.Domain.Contexts;

namespace ApiWithToken.Domain.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;

        public UnitOfWork(DataContext context)
        {
            _context = context;
        }

        public async Task CompleteAsync()
        {
            await _context.SaveChangesAsync();
        }
        
        public void Complate()
        {
            _context.SaveChanges();
        }
    }
}