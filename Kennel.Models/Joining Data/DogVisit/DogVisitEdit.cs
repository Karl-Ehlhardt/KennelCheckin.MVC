using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Joining_Data.DogVisit
{
    public class DogVisitEdit
    {
        public string DogName { get; set; }

        [Required]
        [DisplayName("Drop-Off Date")]
        [DataType(DataType.Date)]
        public DateTime DropOffTime { get; set; }

        [Required]
        [DisplayName("Pick-Up Date")]
        [DataType(DataType.Date)]
        public DateTime PickUpTime { get; set; }

        [MaxLength(300, ErrorMessage = "There are too many characters in this field.")]
        public string Notes { get; set; }
    }
}
