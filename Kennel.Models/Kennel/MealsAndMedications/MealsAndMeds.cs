using KennelData.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Kennel.MealsAndMedications
{
    public class MealsAndMeds
    {
        
        public KennelData.JoiningData.DogInfo DogInfo { get; set; }
        public KennelData.JoiningData.DogVisit DogVisit { get; set; }
        public KennelData.Data.DogBasic DogBasic { get; set; }
        public KennelData.Data.Food Food { get; set; }
        public IEnumerable<KennelData.Data.Medication> MedicationList { get; set; }
        public MealsAndMeds
            (
            KennelData.JoiningData.DogInfo dogInfo,
            KennelData.JoiningData.DogVisit dogVisit,
            KennelData.Data.DogBasic dogBasic,
            KennelData.Data.Food food, 
            IEnumerable<KennelData.Data.Medication> medicationList
            )
        {
            DogInfo = dogInfo;
            DogVisit = dogVisit;
            DogBasic = dogBasic;
            Food = food;
            MedicationList = medicationList;
        }

    }
}
