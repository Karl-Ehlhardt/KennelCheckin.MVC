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
        public KennelData.JoiningData.DogInfo DogInfo { get; set; }
        public KennelData.Data.DogBasic DogBasic { get; set; }
        public KennelData.Data.Food Food { get; set; }
        public DogInfoDetails(KennelData.JoiningData.DogInfo dogInfo, KennelData.Data.DogBasic dogBasic, KennelData.Data.Food food)
        {
            DogInfo = dogInfo;
            DogBasic = dogBasic;
            Food = food;
        }

    }
}
