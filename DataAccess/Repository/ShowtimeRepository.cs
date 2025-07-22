using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class ShowtimeRepository : Repository<Showtime>, IShowtimeRepository
    {
        public ShowtimeRepository(ApplicationDbContext context) : base(context) { }
    }
}