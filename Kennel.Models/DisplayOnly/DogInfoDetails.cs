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
    public class DogInfoDetails
    {
        //Dogs List
        public KennelData.Data.DogBasic DogBasic { get; set; }
        public DogInfoDetails(KennelData.Data.DogBasic dogBasic)
        {
            DogBasic = dogBasic;
        }

    }
}
