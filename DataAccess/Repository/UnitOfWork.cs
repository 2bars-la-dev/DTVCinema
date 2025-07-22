using DataAccess.Data;
using DataAccess.Repository.IRepository;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Concession = new ConcessionRepository(_context);
            ConcessionDetail = new ConcessionDetailRepository(_context);
            Movie = new MovieRepository(_context);
            Order = new OrderRepository(_context);
            Province = new ProvinceRepository(_context);
            Screen = new ScreenRepository(_context);
            Seat = new SeatRepository(_context);
            Showtime = new ShowtimeRepository(_context);
            Theatre = new TheatreRepository(_context);
            Ticket = new TicketRepository(_context);
            TicketDetail = new TicketDetailRepository(_context);
            Transaction = new TransactionRepository(_context);
            User = new UserRepository(_context);
        }
        public IConcessionRepository Concession { get; private set; }
        public IConcessionDetailRepository ConcessionDetail { get; private set; }
        public IMovieRepository Movie { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IProvinceRepository Province { get; private set; }
        public IScreenRepository Screen { get; private set; }
        public ISeatRepository Seat { get; private set; }
        public IShowtimeRepository Showtime { get; private set; }
        public ITheatreRepository Theatre { get; private set; }
        public ITicketRepository Ticket { get; private set; }
        public ITicketDetailRepository TicketDetail { get; private set; }
        public ITransactionRepository Transaction { get; private set; }
        public IUserRepository User { get; private set; }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
