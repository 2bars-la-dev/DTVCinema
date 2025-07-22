using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ConcessionRepository : Repository<Concession>, IConcessionRepository
    {
        public ConcessionRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
