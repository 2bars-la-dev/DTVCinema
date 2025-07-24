using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.DTOs
{
    public class TheatreUpcrateDto
    {
        [Required]
        public int ProvinceId { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class TheatreGetDto
    {
        public int Id { get; set; }
        public int ProvinceId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }
        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;
        public string ProvinceName { get; set; }
    }
}
