using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Food
{
    public class FoodEdit
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double AmountPerMeal { get; set; }

        [Required]
        public bool MorningMeal { get; set; }

        [Required]
        public bool EveningMeal { get; set; }
    }
}
