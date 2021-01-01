using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.JoiningData
{
    public class MedicationToDogInfo
    {
        [Key]
        public int MedicationToDogInfoId { get; set; }

        [Required]
        public int DogInfoId { get; set; }

        [Required]
        public int MedicationId { get; set; }
    }
}
