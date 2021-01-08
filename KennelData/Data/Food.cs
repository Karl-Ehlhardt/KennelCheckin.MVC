using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.Data
{
    public class Food
    {
        [Key]
        public int FoodId { get; set; }

        [Required]
        [Display(Name = "Food Name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Amount Per Meal (Cups)")]
        public double AmountPerMeal { get; set; }

        [Required]
        [Display(Name = "Morning Meal")]
        public bool MorningMeal { get; set; }

        [Required]
        [Display(Name = "Evening Meal")]
        public bool EveningMeal { get; set; }
    }
}
