using System.Text.Json.Serialization;
using Utility;

namespace Models
{
    public class TicketDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ShowtimeId { get; set; }
        public int SeatId { get; set; }

        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }

        public string PaymentMethod { get; set; } = Constant.PaymentMethod_Online;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Order? Order { get; set; }
        public Showtime? Showtime { get; set; }
        public Ticket? Ticket { get; set; }
    }
}
