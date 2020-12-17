using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.Data
{
    public class DogBasic
    {
        [Key]
        public int DogBasicId { get; set; }

        [Required]
        public string DogName { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public double Weight { get; set; }

        [Required]
        public string Size { get; set; }
    }
}
