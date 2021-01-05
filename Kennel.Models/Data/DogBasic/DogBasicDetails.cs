using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.DogBasic
{
    public class DogBasicDetails
    {
        public int DogBasicId { get; set; }

        [Display(Name = "Dog Name")]
        public string DogName { get; set; }
        public string Breed { get; set; }
        [Display(Name = "Weight (Lbs)")]
        public double Weight { get; set; }
    }
}
