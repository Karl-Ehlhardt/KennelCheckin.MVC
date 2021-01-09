using KennelData.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.BillingData
{
    public class DogVisitComplete
    {
        [Key]
        public int DogVisitCompleteId { get; set; }

        [Required]
        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public virtual Owner Owner { get; set; }

        [Required]
        public string DogName { get; set; }

        [Required]
        public DateTimeOffset CheckInTime { get; set; }

        [Required]
        public DateTimeOffset CheckOutTime { get; set; }

        [Required]
        public int TotalHoursOnSite { get; set; }
    }
}
