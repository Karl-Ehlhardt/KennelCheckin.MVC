using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Web;

namespace Kennel.Models.Data.DogImage
{
    public class DogImageCreate
    {
        [Required]
        public HttpPostedFileBase ImgRaw { get; set; }
    }
}
