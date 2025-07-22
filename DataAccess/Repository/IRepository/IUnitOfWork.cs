using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IConcessionRepository Concession { get; }
        IConcessionDetailRepository ConcessionDetail { get; }
        IMovieRepository Movie { get; }
        IOrderRepository Order { get; }
        IProvinceRepository Province { get; }
        IScreenRepository Screen { get; }
        ISeatRepository Seat { get; }
        IShowtimeRepository Showtime { get; }
        ITheatreRepository Theatre { get; }
        ITicketRepository Ticket { get; }
        ITicketDetailRepository TicketDetail { get; }
        ITransactionRepository Transaction { get; }
        IUserRepository User { get; }
        Task SaveAsync();
    }
}
