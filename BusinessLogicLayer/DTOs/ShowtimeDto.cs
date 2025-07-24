using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs
{
    public class ShowtimeUpcrateDto
    {
        [Required]
        public int MovieId { get; set; }

        [Required]
        public int ScreenId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        [Range(0, 1000000)]
        public decimal Price { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class ShowtimeGetDto
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public string MovieTitle { get; set; } = string.Empty;
        public int ScreenId { get; set; }
        public string ScreenName { get; set; } = string.Empty;
        public string TheatreName { get; set; } = string.Empty;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
    }
}