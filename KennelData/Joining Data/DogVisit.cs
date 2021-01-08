using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        [DisplayName("Drop-Off Date")]
        [DataType(DataType.Date)]
        public DateTime DropOffTime { get; set; }

        [Required]
        [DisplayName("Pick-Up Date")]
        [DataType(DataType.Date)]
        public DateTime PickUpTime { get; set; }

        public string Notes { get; set; }

        public DateTimeOffset CheckInTime { get; set; }

        public bool OnSite { get; set; }

        public int TotalHoursOnSite { get; set; }
    }
}
