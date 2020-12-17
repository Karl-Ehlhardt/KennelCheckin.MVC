using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Food
{
    public class FoodDetails
    {
        public string Name { get; set; }

        public double AmountPerMeal { get; set; }

        public bool MorningMeal { get; set; }

        public bool EveningMeal { get; set; }
    }
}
