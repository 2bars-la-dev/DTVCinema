using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Screen
    {
        public int Id { get; set; }
        public int TheatreId { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public int RowNum { get; set; }
        public int ColNum { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; } = true;

        public Theatre? Theatre { get; set; }

        public ICollection<Seat> Seats { get; set; } = new List<Seat>();

        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
}
