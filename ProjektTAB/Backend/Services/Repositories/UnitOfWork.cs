using Database;
using Database.Repositories;

namespace Backend.Services.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ClinicContext _context;

        public UnitOfWork(ClinicContext context)
        {
            _context = context;
        }

        public IUserRepository Users { get; private set; }

        /*
         * More repositories should be added here 
         * 
         */

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
