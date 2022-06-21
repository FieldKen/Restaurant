using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class MealEditViewModel
    {
        [Required]
        [DisplayName("Naam")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Prijs")]
        public double Price { get; set; }
        [MaxLength(100)]
        [DisplayName("Beschrijving")]
        public string Description { get; set; }

        [DisplayName("Foto")]
        public IFormFile Photo { get; set; }

        public string? PhotoUrl { get; set; }
    }
}
