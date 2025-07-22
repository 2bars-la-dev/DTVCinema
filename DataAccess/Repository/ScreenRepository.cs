using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class ScreenRepository : Repository<Screen>, IScreenRepository
    {
        public ScreenRepository(ApplicationDbContext context) : base(context) { }
    }
}