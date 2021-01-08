using KennelData.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Kennel.KennelDashboardItems
{
    public class KennelDogDetails
    {
        
        public KennelData.JoiningData.DogInfo DogInfo { get; set; }
        public KennelData.JoiningData.DogVisit DogVisit { get; set; }
        public KennelData.Data.DogBasic DogBasic { get; set; }
        public KennelData.Data.Owner Owner { get; set; }
        public KennelData.Data.Food Food { get; set; }
        public KennelData.Data.Special Special { get; set; }
        public KennelData.Data.Vet Vet { get; set; }
        public IEnumerable<KennelData.Data.Medication> MedicationList { get; set; }
        public KennelDogDetails
            (
            KennelData.JoiningData.DogInfo dogInfo,
            KennelData.JoiningData.DogVisit dogVisit,
            KennelData.Data.DogBasic dogBasic,
            KennelData.Data.Owner owner,
            KennelData.Data.Food food, 
            KennelData.Data.Special special, 
            KennelData.Data.Vet vet,
            IEnumerable<KennelData.Data.Medication> medicationList
            )
        {
            DogInfo = dogInfo;
            DogVisit = dogVisit;
            DogBasic = dogBasic;
            Owner = owner;
            Food = food;
            Special = special;
            Vet = vet;
            MedicationList = medicationList;
        }

    }
}
