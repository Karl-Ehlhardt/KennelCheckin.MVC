using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Vet
{
    public class VetCreate
    {
        [Required]
        public string BusinessName { get; set; }

        [Required]
        public string VetName { get; set; }

        [Required]
        public string Phone { get; set; }
    }
}
