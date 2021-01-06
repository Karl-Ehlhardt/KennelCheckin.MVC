using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Joining_Data.DogVisit
{
    public class DogVisitListItem
    {
        public int DogVisitId { get; set; }

        public string DogName { get; set; }

        [DisplayName("Drop-Off Date")]
        [DataType(DataType.Date)]
        public DateTime DropOffTime { get; set; }

        [DisplayName("Pick-Up Date")]
        [DataType(DataType.Date)]
        public DateTime PickUpTime { get; set; }

        public string Notes { get; set; }
    }
}
