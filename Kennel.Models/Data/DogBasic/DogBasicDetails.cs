using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.DogBasic
{
    public class DogBasicDetails
    { 
        public string DogName { get; set; }

        public string Breed { get; set; }

        public double Weight { get; set; }

        public string Size { get; set; }
    }
}
