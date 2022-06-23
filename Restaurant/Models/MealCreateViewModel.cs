using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Restaurant.Models
{
    public class MealCreateViewModel
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
        [DisplayName("Type")]
        public string MealType { get; set; }

        public IEnumerable<SelectListItem> MealTypes { get; set; } = new List<SelectListItem>()
        {
            new SelectListItem("Voorgerecht", "Voorgerecht"),
            new SelectListItem("Hoofdgerecht", "Hoofdgerecht"),
            new SelectListItem("Nagerecht", "Nagerecht")
        };
    }
}
