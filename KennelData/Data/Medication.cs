﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.Data
{
    public class Medication
    {
        [Key]
        public int MedicationId { get; set; }

        [Required]
        [Display(Name = "Medication Name")]
        public string Name { get; set; }

        [Required]
        public string Dose { get; set; }

        [Required]
        [Display(Name = "Takes with Morning Meal")]
        public bool MorningMeal { get; set; }

        [Required]
        [Display(Name = "Takes with Evening Meal")]
        public bool EveningMeal { get; set; }

        [Required]
        public string Instructions { get; set; }
    }
}
