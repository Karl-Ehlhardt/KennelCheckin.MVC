using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Kennel.KennelDashboardItems
{
    public class KennelDashboardDogListItem
    {
        public int DogInfoId { get; set; }

        public int DogVisitId { get; set; }

        public string DogName { get; set; }

        public string SpecialInstructions { get; set; }

        public string Allergies { get; set; }

        public string VisitNotes { get; set; }

        public KennelDashboardDogListItem
        (
            int dogInfoId,
            int dogVisitId,
            string dogName,
            string specialInstructions,
            string allergies,
            string visitNotes
        )
        {
            DogInfoId = dogInfoId;
            DogVisitId = dogVisitId;
            DogName = dogName;
            SpecialInstructions = specialInstructions;
            Allergies = allergies;
            VisitNotes = visitNotes;
        }
    }
}
