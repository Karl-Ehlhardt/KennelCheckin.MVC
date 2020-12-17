using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.Data
{
    public class Special
    {
        [Key]
        public int SpecialId { get; set; }

        [Required]
        public string Instructions { get; set; }

        [Required]
        public string Allergies { get; set; }
    }
}
