using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.Data
{
    public class Vet
    {
        [Key]
        public int VetId { get; set; }

        [Required]
        [Display(Name = "Business Name")]
        public string BusinessName { get; set; }

        [Required]
        [Display(Name = "Veterinarian Name")]
        public string VetName { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
