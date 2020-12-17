using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KennelData.Data
{
    public class DogImage
    {
        [Key]
        public int DogImageId { get; set; }

        [Required]
        public byte[] ImgFile { get; set; }
    }
}
