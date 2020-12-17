using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Joining_Data.DogInfo
{
    public class DogInfoEdit
    {
        public int FoodId { get; set; }

        public int SpecialId { get; set; }

        public int VetId { get; set; }

        public List<int> MedicationIdList { get; set; }
    }
}
