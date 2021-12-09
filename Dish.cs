using System;
using System.ComponentModel.DataAnnotations;


namespace CRUDelicious.Models.Home
{
    public class Dish
    {
        [Key]
        public int DishId {get; set; }

        [Required(ErrorMessage = "Please provide a name")]
        [MinLength(5, ErrorMessage = "Please provide a name with at least 5 characters")]
        public string Name {get; set; }

        [Required(ErrorMessage = "Please provide a chef")]
        [MinLength(5, ErrorMessage = "Please provide a chef with at least 5 characters")]
        public string Chef {get; set; }

        [Required(ErrorMessage = "Please provide a tastiness level")]
        [Range(0,5, ErrorMessage = "Please follow instructions")]

        public int Tastiness {get; set; }

        [Required(ErrorMessage = "Please provide a caloric estimation")]
        [Range(0,3500, ErrorMessage = "Please seek the aid of a nutritionist")]
        public int Calories {get; set; }

        [Required(ErrorMessage = "Please provide a description of your meal")]
        [MinLength(20, ErrorMessage = "Please provide a comment with at least 20 characters")]

        public string Description {get; set; }
        public DateTime CreatedAt {get; set; } = DateTime.Now;
        public DateTime UpdatedAt {get; set; } = DateTime.Now;
    }
}