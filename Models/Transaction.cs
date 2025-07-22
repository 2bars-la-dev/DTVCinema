using System.Text.Json.Serialization;
using Utility;

namespace Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public string PaymentProvider { get; set; } = string.Empty;
        public string TransactionReference { get; set; } = string.Empty;
        public string Status { get; set; } = Constant.TransactionStatus_Pending;
        public DateTime CreatedAt { get; set; } = TimeHelper.GetVietnamTime();
        public DateTime? UpdatedAt { get; set; }

        public Order? Order { get; set; }
    }
}
