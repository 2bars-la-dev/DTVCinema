using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class TicketDetailRepository : Repository<TicketDetail>, ITicketDetailRepository
    {
        public TicketDetailRepository(ApplicationDbContext context) : base(context) { }
    }
}