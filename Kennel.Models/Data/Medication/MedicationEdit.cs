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
        public bool MorningMeal { get; set; }

        [Required]
        public bool EveningMeal { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}
