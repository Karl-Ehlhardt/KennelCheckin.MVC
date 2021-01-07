using Kennel.Data.Users;
using Kennel.Models.Data.Kennel.KennelDashboardItems;
using Kennel.Models.Joining_Data.DogVisit;
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

            List<DogVisit> incomingDogVisitList =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == false && q.HoursOnSite == 0)
                .Select(
                q =>
                new DogVisit()
                {
                    DogVisitId = q.DogVisitId,
                    DogInfoId = q.DogInfoId,
                    Notes = q.Notes
                }
                ).ToListAsync();

            //intilize list

            //Look up dog basic for name and special in for loop and add everything together


            List<DogBasic> dogBasic = new List<DogBasic>();
            List<DogVisitListItem> dogFuture = new List<DogVisitListItem>();
            List<DogVisitListItem> dogOnGoing = new List<DogVisitListItem>();

            var dogVistService = new DogVisitHelperService(_userId);

            foreach (Kennel dogIn in kennel)
            {
                DogBasic item =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogIn.DogBasicId);
                dogBasic.Add(item);

                dogFuture.AddRange(await dogVistService.GetAllFutureDogVisits(dogIn.KennelId, item.DogName));
                dogOnGoing.AddRange(await dogVistService.GetAllOngoingDogVisits(dogIn.KennelId, item.DogName));
            }

            KennelIndexView model = new KennelIndexView(dogBasic.AsEnumerable(), owner, kennel, dogFuture.AsEnumerable(), dogOnGoing.AsEnumerable());

            return model;
        }

        //public async Task<KennelDetails> GetKennelById(int id)
        //{
        //    Kennel kennel =
        //        await
        //        _context
        //        .Kennels
        //        .SingleAsync(q => q.KennelId == id);

        //    DogBasic dogBasic =
        //        await
        //        _context
        //        .DogBasics
        //        .SingleAsync(q => q.DogBasicId == kennel.DogBasicId);

        //    Food food =
        //        await
        //        _context
        //        .Foods
        //        .SingleOrDefaultAsync(q => q.FoodId == kennel.FoodId);

        //    Special special =
        //        await
        //        _context
        //        .Specials
        //        .SingleOrDefaultAsync(q => q.SpecialId == kennel.SpecialId);

        //    Vet vet =
        //        await
        //        _context
        //        .Vets
        //        .SingleOrDefaultAsync(q => q.VetId == kennel.VetId);

        //    List<MedicationToKennel> medicationToKennelList =
        //        await
        //        _context
        //        .MedicationToKennels
        //        .Where(q => q.KennelId == kennel.KennelId).ToListAsync();

        //    List<Medication> medicationList = new List<Medication>();

        //    if (medicationToKennelList.Count() > 0)
        //    {
        //        foreach (MedicationToKennel item in medicationToKennelList)
        //        {
        //            Medication medication =
        //            await
        //            _context
        //            .Medications
        //            .SingleOrDefaultAsync(q => q.MedicationId == item.MedicationId);
        //            medicationList.Add(medication);
        //        }
        //    }

        //    KennelDetails model = new KennelDetails(kennel, dogBasic, food, special, vet, medicationList.AsEnumerable());

        //    return model;
        //}

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
