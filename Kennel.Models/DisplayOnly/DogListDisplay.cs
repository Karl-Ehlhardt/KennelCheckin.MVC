using KennelData.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.DisplayOnly
{
    public class DogListDisplay
    {

        [ForeignKey(nameof(DogBasic))]
        public int DogId { get; set; }

        public virtual KennelData.Data.DogBasic DogBasic { get; set; }

        [ForeignKey(nameof(DogImage))]
        public int DogImageId { get; set; }

        public virtual KennelData.Data.DogImage DogImage { get; set; }
    }
}
