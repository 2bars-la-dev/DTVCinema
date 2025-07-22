using System.Text.Json.Serialization;
using Utility;

namespace Models
{
    public class ConcessionDetail
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public int ConcessionId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public string PaymentMethod { get; set; } = Constant.PaymentMethod_Offline;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public Order Order { get; set; }

        public Concession Concession { get; set; }
    }
}
