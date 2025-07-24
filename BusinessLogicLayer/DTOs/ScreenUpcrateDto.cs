using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs
{
    public class ScreenUpcrateDto
    {
        [Required]
        public int TheatreId { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        [Range(1, 100)]
        public int RowNum { get; set; }

        [Range(1, 100)]
        public int ColNum { get; set; }

        public bool IsActive { get; set; } = true;
    }

    public class ScreenGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public int TheatreId { get; set; }
        public string? TheatreName { get; set; }

        public int RowNum { get; set; }
        public int ColNum { get; set; }
        public int Capacity { get; set; }

        public bool IsActive { get; set; }
    }
}