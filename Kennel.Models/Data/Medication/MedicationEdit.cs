using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Medication
{
    public class MedicationEdit
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public double Dose { get; set; }

        [Required]
        [Display(Name = "Has this with the morning meal?")]
        public bool MorningMeal { get; set; }

        [Required]
        [Display(Name = "Has this with the evening meal?")]
        public bool EveningMeal { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}
