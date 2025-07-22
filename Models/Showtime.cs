using System.Text.Json.Serialization;

namespace Models
{
    public class Showtime
    {
        public int Id { get; set; }

        public int MovieId { get; set; }
        public int ScreenId { get; set; }

        public decimal Price { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public bool IsActive { get; set; } = true;

        public Movie? Movie { get; set; }

        public Screen? Screen { get; set; }

        public ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();
    }
}
