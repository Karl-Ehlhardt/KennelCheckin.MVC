using Kennel.Models.Joining_Data.DogVisit;
using Kennel.Models.Kennel.KennelDashboardItems;
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
    public class KennelDashboardView
    {
        public IEnumerable<KennelDashboardDogListItem> DogsOnSite { get; set; }
        public IEnumerable<KennelDashboardDogListItem> IncomingDogs { get; set; }
        public IEnumerable<KennelDashboardDogListItem> OutgoingDogs { get; set; }

        public KennelDashboardView
            (
            IEnumerable<KennelDashboardDogListItem> dogsOnSite,
            IEnumerable<KennelDashboardDogListItem> incomingDogs,
            IEnumerable<KennelDashboardDogListItem> outgoingDogs
            )
        {
            DogsOnSite = dogsOnSite;
            IncomingDogs = incomingDogs;
            OutgoingDogs = outgoingDogs;
        }

    }
}
