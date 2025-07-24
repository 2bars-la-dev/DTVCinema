using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs
{
    public class MovieUpcrateDto
    {
        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Director { get; set; }
        public string? Actors { get; set; }
        public string? Description { get; set; }

        [Required, Range(1, 500)]
        public int Duration { get; set; }

        public DateTime? ReleaseDate { get; set; }

        public string? Genres { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class MovieGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Director { get; set; }
        public string? Actors { get; set; }
        public string? Description { get; set; }
        public int Duration { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public string? Genres { get; set; }
        public string? PosterUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}