using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.JoiningData
{
    public class DogVisit
    {
        [Key]
        public int DogVisitId { get; set; }

        [Required]
        public int DogInfoId { get; set; }

        [Required]
        public DateTime DropOffTime { get; set; }

        [Required]
        public DateTime PickUpTime { get; set; }

        public string Notes { get; set; }
    }
}
