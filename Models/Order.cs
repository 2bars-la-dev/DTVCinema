using System.Text.Json.Serialization;
using Utility;

namespace Models
{
    public class Order
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }

        public string Status { get; set; } = Constant.OrderStatus_Pending;
        public string? TransactionId { get; set; }

        public DateTime CreatedAt { get; set; } = TimeHelper.GetVietnamTime();
        public DateTime? UpdatedAt { get; set; }

        public ICollection<TicketDetail> TicketDetails { get; set; } = new List<TicketDetail>();

        public ICollection<ConcessionDetail> ConcessionDetails { get; set; } = new List<ConcessionDetail>();

        public User? User { get; set; }
    }
}
