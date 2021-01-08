using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Joining_Data.DogVisit
{
    public class DogVisitChangePickup
    {
        public string DogName { get; set; }

        [Required]
        [DisplayName("New Pick-Up Date")]
        [DataType(DataType.Date)]
        public DateTime PickUpTime { get; set; }

    }
}
