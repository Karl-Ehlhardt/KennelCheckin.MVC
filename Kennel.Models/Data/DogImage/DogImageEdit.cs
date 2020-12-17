using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.DogImage
{
    public class DogImageEdit
    {
        [Required]
        public byte[] ImgFile { get; set; }
    }
}
