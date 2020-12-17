using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Joining_Data.DogInfo
{
    public class DogInfoCreate
    {
        [Required]
        public int DogBasicId { get; set; }

        [Required]
        public int OwnerId { get; set; }
    }
}
