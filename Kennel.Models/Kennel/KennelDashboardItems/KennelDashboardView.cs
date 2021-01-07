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
        public IEnumerable<KennelDashdoardDogListItem> DogsOnSite { get; set; }
        public IEnumerable<KennelDashdoardDogListItem> IncomingDogs { get; set; }
        public IEnumerable<KennelDashdoardDogListItem> OutgoingDogs { get; set; }

        public KennelDashboardView
            (
            IEnumerable<KennelDashdoardDogListItem> dogsOnSite,
            IEnumerable<KennelDashdoardDogListItem> incomingDogs,
            IEnumerable<KennelDashdoardDogListItem> outgoingDogs
            )
        {
            DogsOnSite = dogsOnSite;
            IncomingDogs = incomingDogs;
            OutgoingDogs = outgoingDogs;
        }

    }
}
