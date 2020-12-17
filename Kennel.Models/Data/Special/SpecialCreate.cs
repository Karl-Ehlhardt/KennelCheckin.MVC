using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Special
{
    public class SpecialCreate
    {
        [Required]
        public string Instructions { get; set; }

        [Required]
        public string Allergies { get; set; }
    }
}
