using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;

namespace DataAccess.Repository
{
    public class ConcessionDetailRepository : Repository<ConcessionDetail>, IConcessionDetailRepository
    {
        public ConcessionDetailRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
