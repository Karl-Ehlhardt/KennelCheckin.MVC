using Kennel.Data.Users;
using Kennel.Models.Data.Kennel.KennelDashboardItems;
using Kennel.Models.Joining_Data.DogVisit;
using Kennel.Models.Kennel.KennelDashboardItems;
using Kennel.Service.Shared;
using KennelData.Data;
using KennelData.JoiningData;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Kennel.Service.Kennel
{
    public class KennelService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public KennelService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CheckOwnerExists()
        {
            Owner owner =
                await
                _context
                .Owners
                .SingleOrDefaultAsync(a => a.ApplicationUserId == _userId.ToString());

            return owner == null;
        }

        ////Create new kennel
        //public async Task<int> CreateKennel()
        //{
        //    DogBasic dogBasic =
        //        _context
        //        .DogBasics.OrderByDescending(p => p.DogBasicId)
        //        .FirstOrDefault();

        //    Owner owner =
        //        _context
        //        .Owners
        //        .Single(a => a.ApplicationUserId == _userId.ToString());

        //    Kennel kennel =
        //        new Kennel()
        //        {
        //            DogBasicId = dogBasic.DogBasicId,
        //            OwnerId = owner.OwnerId
        //        };

        //    _context.Kennels.Add(kennel);
        //    if (await _context.SaveChangesAsync() == 1)
        //    {
        //        return kennel.KennelId;
        //    }
        //    return 0;
        //}

        // kennel Dashboard Display
        public async Task<KennelDashboardView> DisplayKennelDashboardView()
        {
            //On Site Dogs
            List<DogVisit> onSiteDogVisitList =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == true)
                .ToListAsync();

            List<KennelDashboardDogListItem> onSiteDogs = new List<KennelDashboardDogListItem>();

            foreach (DogVisit dogVisit in onSiteDogVisitList)
            {
                DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

                DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

                Special special = new Special();

                if (dogInfo.SpecialId > 0)
                {
                    special =
                    await
                    _context
                    .Specials
                    .SingleAsync(q => q.SpecialId == dogInfo.SpecialId);
                }


                onSiteDogs.Add(new KennelDashboardDogListItem(dogInfo.DogInfoId, dogVisit.DogVisitId, dogBasic.DogName, special.Instructions, special.Allergies, dogVisit.Notes));
            }

            //DateTime thisDay = DateTime.Today;
            //Incoming Dogs

            List<DogVisit> incomingDogVisitList =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == false && q.TotalHoursOnSite == 0 && q.DropOffTime == DateTime.Today)
                .ToListAsync();

            List<KennelDashboardDogListItem> incomingDogs = new List<KennelDashboardDogListItem>();

            foreach (DogVisit dogVisit in incomingDogVisitList)
            {
                DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

                DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

                Special special = new Special();

                if (dogInfo.SpecialId > 0)
                {
                special =
                await
                _context
                .Specials
                .SingleAsync(q => q.SpecialId == dogInfo.SpecialId);
                }

                incomingDogs.Add(new KennelDashboardDogListItem(dogInfo.DogInfoId, dogVisit.DogVisitId, dogBasic.DogName, special.Instructions, special.Allergies, dogVisit.Notes));
            }

            //Outgoing Dogs
            List<DogVisit> outgoingDogVisitList =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == true && q.PickUpTime >= DateTime.Today)
                .ToListAsync();

            List<KennelDashboardDogListItem> outgoingDogs = new List<KennelDashboardDogListItem>();

            foreach (DogVisit dogVisit in outgoingDogVisitList)
            {
                DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

                DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

                Special special = new Special();

                if (dogInfo.SpecialId > 0)
                {
                    special =
                    await
                    _context
                    .Specials
                    .SingleAsync(q => q.SpecialId == dogInfo.SpecialId);
                }

                outgoingDogs.Add(new KennelDashboardDogListItem(dogInfo.DogInfoId, dogVisit.DogVisitId, dogBasic.DogName, special.Instructions, special.Allergies, dogVisit.Notes));
            }



            KennelDashboardView model = new KennelDashboardView(onSiteDogs.AsEnumerable(), incomingDogs.AsEnumerable(), outgoingDogs.AsEnumerable());

            return model;
        }

        public async Task<KennelDogDetails> KennelDogDetailsByDogVisitId(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(q => q.DogVisitId == id);

            DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

            DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

            Owner owner =
                await
                _context
                .Owners
                .SingleAsync(q => q.OwnerId == dogInfo.OwnerId);

            Food food =
                await
                _context
                .Foods
                .SingleOrDefaultAsync(q => q.FoodId == dogInfo.FoodId);

            Special special =
                await
                _context
                .Specials
                .SingleOrDefaultAsync(q => q.SpecialId == dogInfo.SpecialId);

            Vet vet =
                await
                _context
                .Vets
                .SingleOrDefaultAsync(q => q.VetId == dogInfo.VetId);

            List<MedicationToDogInfo> medicationToDogInfoList =
                await
                _context
                .MedicationToDogInfos
                .Where(q => q.DogInfoId == dogInfo.DogInfoId).ToListAsync();

            List<Medication> medicationList = new List<Medication>();

            if (medicationToDogInfoList.Count() > 0)
            {
                foreach (MedicationToDogInfo item in medicationToDogInfoList)
                {
                    Medication medication =
                    await
                    _context
                    .Medications
                    .SingleOrDefaultAsync(q => q.MedicationId == item.MedicationId);
                    medicationList.Add(medication);
                }
            }

            KennelDogDetails model = new KennelDogDetails(dogInfo, dogVisit, dogBasic, owner, food, special, vet, medicationList.AsEnumerable());

            return model;
        }

        public async Task<DogBasic> GetDogBasicByDogVisitId(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(q => q.DogVisitId == id);

            DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == dogVisit.DogInfoId);

            DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

            return dogBasic;
        }

        public async Task<bool> CheckInDogVisitById(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(q => q.DogVisitId == id);
            dogVisit.OnSite = true;
            dogVisit.CheckInTime = DateTimeOffset.Now;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> CheckOutDogVisitById(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(q => q.DogVisitId == id);
            dogVisit.OnSite = false;
            dogVisit.TotalHoursOnSite = Convert.ToInt32(Math.Ceiling((DateTimeOffset.Now - dogVisit.CheckInTime).TotalHours));

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> ResetDogVisitById(int id)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleOrDefaultAsync(q => q.DogVisitId == id && q.TotalHoursOnSite == 0);

            if(dogVisit == null)
            {
                return false;
            }

            dogVisit.OnSite = false;
            dogVisit.CheckInTime = new DateTime();

            return await _context.SaveChangesAsync() == 1;
        }



        ////Update kennel by id
        //public async Task<bool> UpdateKennelAdd(int kennelId, string mark)
        //{
        //    Kennel kennel =
        //            _context
        //            .Kennels
        //            .Single(a => a.KennelId == kennelId);

        //    switch (mark)
        //    {
        //        case "Food":
        //            Food food =
        //                await
        //            _context
        //            .Foods.OrderByDescending(p => p.FoodId)
        //            .FirstOrDefaultAsync();
        //            kennel.FoodId = food.FoodId;
        //            break;
        //        case "Special":
        //            Special special =
        //                await
        //            _context
        //            .Specials.OrderByDescending(p => p.SpecialId)
        //            .FirstOrDefaultAsync();
        //            kennel.SpecialId = special.SpecialId;
        //            break;
        //        case "Vet":
        //            Vet vet =
        //                await
        //            _context
        //            .Vets.OrderByDescending(p => p.VetId)
        //            .FirstOrDefaultAsync();
        //            kennel.VetId = vet.VetId;
        //            break;
        //        case "DogImage":
        //            DogImage dogImage =
        //                await
        //            _context
        //            .DogImages.OrderByDescending(p => p.DogImageId)
        //            .FirstOrDefaultAsync();
        //            kennel.DogImageId = dogImage.DogImageId;
        //            break;
        //        default:
        //            return false;
        //    }

        //    return await _context.SaveChangesAsync() == 1;
        //}

        //public async Task<bool> UpdateKennelRemove(int id, string mark)
        //{
        //    switch (mark)
        //    {
        //        case "Food":
        //            Kennel kennelFood =
        //                await
        //                _context
        //                .Kennels
        //                .SingleAsync(a => a.FoodId == id);
        //            kennelFood.FoodId = 0;
        //            return await _context.SaveChangesAsync() == 1;
        //        case "Special":
        //            Kennel kennelSpecial =
        //                await
        //                _context
        //                .Kennels
        //                .SingleAsync(a => a.SpecialId == id);
        //            kennelSpecial.SpecialId = 0;
        //            return await _context.SaveChangesAsync() == 1;
        //        case "Vet":
        //            Kennel kennelVet =
        //                await
        //                _context
        //                .Kennels
        //                .SingleAsync(a => a.VetId == id);
        //            kennelVet.VetId = 0;
        //            return await _context.SaveChangesAsync() == 1;
        //        case "DogImage":
        //            Kennel kennelImage =
        //                await
        //                _context
        //                .Kennels
        //                .SingleAsync(a => a.DogImageId == id);
        //            kennelImage.DogImageId = 0;
        //            return await _context.SaveChangesAsync() == 1;
        //        default:
        //            break;
        //    }

        //    return await _context.SaveChangesAsync() == 1;
        //}
        //public async Task<bool> DeleteKennel(int id)
        //{
        //    var kennel =
        //        _context
        //        .Kennels
        //        .Single(e => e.KennelId == id);
        //    //Clean up data base

        //    _context.Kennels.Remove(kennel);

        //    return await _context.SaveChangesAsync() == 1;
        //}
    }

}
