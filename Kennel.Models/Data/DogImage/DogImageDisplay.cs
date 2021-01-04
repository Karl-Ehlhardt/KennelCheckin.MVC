using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Kennel.Models.Data.DogImage
{
    public class DogImageDisplay
    {
        public int DogImageId { get; set; }
        public Image ImgOut { get; set; }
    }
}
