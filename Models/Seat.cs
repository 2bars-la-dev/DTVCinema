using System.Text.Json.Serialization;
using Utility;

namespace Models
{
    public class Seat
    {
        public int Id { get; set; }
        public int ScreenId { get; set; }

        public int Row { get; set; }
        public int Number { get; set; }

        public string Status { get; set; } = Constant.SeatStatus_Available;
        public bool IsActive { get; set; } = true;

        public Screen? Screen { get; set; }
    }
}
