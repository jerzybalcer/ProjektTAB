using Database;
using Database.Repositories;
using Database.Users;

namespace Backend.Services.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ClinicContext context) : base(context)
        {
        }
    }
}
