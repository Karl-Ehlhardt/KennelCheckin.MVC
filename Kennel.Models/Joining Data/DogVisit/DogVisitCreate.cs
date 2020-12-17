using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Joining_Data.DogVisit
{
    public class DogVisitCreate
    {
        [Required]
        public int DogInfoId { get; set; }

        [Required]
        public DateTime DropOffTime { get; set; }

        [Required]
        public DateTime PickUpTime { get; set; }

        public string Notes { get; set; }
    }
}
