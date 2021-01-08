using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Food
{
    public class FoodCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Amount Per Meal(Cups)")]
        public double AmountPerMeal { get; set; }

        [Required]
        [Display(Name = "Eats in the Morning?")]
        public bool MorningMeal { get; set; }

        [Required]
        [Display(Name = "Eats in the Evening?")]
        public bool EveningMeal { get; set; }
    }
}
