using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.JoiningData
{
    public class DogInfo
    {
        [Key]
        public int DogInfoId { get; set; }

        [Required]
        public int DogBasicId { get; set; }

        [Required]
        public int OwnerId { get; set; }

        public int FoodId { get; set; }

        public int SpecialId { get; set; }

        public int VetId { get; set; }

        public int DogImageId { get; set; }
    }
}
