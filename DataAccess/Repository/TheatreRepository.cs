using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class TheatreRepository : Repository<Theatre>, ITheatreRepository
    {
        public TheatreRepository(ApplicationDbContext context) : base(context) { }
    }
}