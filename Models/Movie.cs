using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Utility;

namespace Models
{
    public class Movie
    {
        public int Id { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Director { get; set; }
        public string? Actors { get; set; }
        public string? Description { get; set; }

        [Range(1, 500)]
        public int Duration { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? Genres { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = TimeHelper.GetVietnamTime();
        public DateTime? UpdatedAt { get; set; }

        public ICollection<Showtime> Showtimes { get; set; } = new List<Showtime>();
    }
}
