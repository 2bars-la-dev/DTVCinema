using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Utility;

namespace Models
{
    public class Theatre
    {
        public int Id { get; set; }

        public int ProvinceId { get; set; }

        [Required, StringLength(200)]
        public string Name { get; set; } = string.Empty;

        public string? Address { get; set; }
        public string? Phone { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } = TimeHelper.GetVietnamTime();
        public DateTime? UpdatedAt { get; set; }

        public Province? Province { get; set; }

        public ICollection<Screen> Screens { get; set; } = new List<Screen>();
    }
}
