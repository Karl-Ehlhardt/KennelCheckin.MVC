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
        public string Name { get; set; }

        [Required]
        public double AmountPerMeal { get; set; }

        [Required]
        public bool MorningMeal { get; set; }

        [Required]
        public bool EveningMeal { get; set; }
    }
}
