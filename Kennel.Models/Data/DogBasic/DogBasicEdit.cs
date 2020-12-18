using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.DogBasic
{
    public class DogBasicEdit
    {
        [Required]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        [Display(Name = "Dog Name")]
        public string DogName { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Breed { get; set; }

        [Required]
        [Display(Name = "Weight (Lbs)")]
        public double Weight { get; set; }

        [Required]
        [MaxLength(100, ErrorMessage = "There are too many characters in this field.")]
        public string Size { get; set; }
    }
}
