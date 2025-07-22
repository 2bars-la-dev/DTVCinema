using System.Text.Json.Serialization;

namespace Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public int TicketDetailId { get; set; }

        public int ShowtimeId { get; set; }

        // Navigation
        public TicketDetail? TicketDetail { get; set; }
        public Showtime? Showtime { get; set; }
    }
}
