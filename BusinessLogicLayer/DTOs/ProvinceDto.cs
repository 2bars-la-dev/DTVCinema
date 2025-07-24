using System.ComponentModel.DataAnnotations;

namespace BusinessLogicLayer.DTOs
{
    public class ProvinceDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
    }
}
