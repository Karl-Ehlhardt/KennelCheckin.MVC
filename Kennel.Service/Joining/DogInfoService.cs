using Kennel.Data.Users;
using Kennel.Models.Data.Joining_Data.DogInfo.DisplayOnly;
using Kennel.Models.Joining_Data.DogInfo;
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

namespace Kennel.Service.Joining
{
    public class DogInfoService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public DogInfoService(Guid userId)
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

        //Create new dogInfo
        public async Task<int> CreateDogInfo()
        {
            DogBasic dogBasic =
                _context
                .DogBasics.OrderByDescending(p => p.DogBasicId)
                .FirstOrDefault();

            Owner owner =
                _context
                .Owners
                .Single(a => a.ApplicationUserId == _userId.ToString());

            DogInfo dogInfo =
                new DogInfo()
                {
                    DogBasicId = dogBasic.DogBasicId,
                    OwnerId = owner.OwnerId
                };

            _context.DogInfos.Add(dogInfo);
            if (await _context.SaveChangesAsync() == 1)
            {
                return dogInfo.DogInfoId;
            }
            return 0;
        }

        // dogInfo Display
        public async Task<DogInfoIndexView> DisplayDogInfoIndexView()
        {
            Owner owner =
                _context
                .Owners
                .SingleOrDefault(a => a.ApplicationUserId == _userId.ToString());

            IEnumerable<DogInfo> dogInfo =
                await
                _context
                .DogInfos
                .Where(q => q.OwnerId == owner.OwnerId)
                .ToListAsync();


            List<DogBasic> dogBasic = new List<DogBasic>();
            List<DogVisitListItem> dogFuture = new List<DogVisitListItem>();
            List<DogVisitListItem> dogOnGoing = new List<DogVisitListItem>();

            var dogVistService = new DogVisitHelperService(_userId);

            foreach (DogInfo dogIn in dogInfo)
            {
                DogBasic item =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogIn.DogBasicId);
                dogBasic.Add(item);

                dogFuture.AddRange(await dogVistService.GetAllFutureDogVisits(dogIn.DogInfoId, item.DogName));
                dogOnGoing.AddRange(await dogVistService.GetAllOngoingDogVisits(dogIn.DogInfoId, item.DogName));
            }

            DogInfoIndexView model = new DogInfoIndexView(dogBasic.AsEnumerable(), owner, dogInfo, dogFuture.AsEnumerable(), dogOnGoing.AsEnumerable());

            return model;
        }

        public async Task<DogInfoDetails> GetDogInfoById(int id)
        {
            DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(q => q.DogInfoId == id);

            DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

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

            DogInfoDetails model = new DogInfoDetails(dogInfo, dogBasic, food, special, vet, medicationList.AsEnumerable());

            return model;
        }

        //Update dogInfo by id
        public async Task<bool> UpdateDogInfoAdd(int dogInfoId, string mark)
        {
            DogInfo dogInfo =
                    _context
                    .DogInfos
                    .Single(a => a.DogInfoId == dogInfoId);

            switch (mark)
            {
                case "Food":
                    Food food =
                        await
                    _context
                    .Foods.OrderByDescending(p => p.FoodId)
                    .FirstOrDefaultAsync();
                    dogInfo.FoodId = food.FoodId;
                    break;
                case "Special":
                    Special special =
                        await
                    _context
                    .Specials.OrderByDescending(p => p.SpecialId)
                    .FirstOrDefaultAsync();
                    dogInfo.SpecialId = special.SpecialId;
                    break;
                case "Vet":
                    Vet vet =
                        await
                    _context
                    .Vets.OrderByDescending(p => p.VetId)
                    .FirstOrDefaultAsync();
                    dogInfo.VetId = vet.VetId;
                    break;
                case "DogImage":
                    DogImage dogImage =
                        await
                    _context
                    .DogImages.OrderByDescending(p => p.DogImageId)
                    .FirstOrDefaultAsync();
                    dogInfo.DogImageId = dogImage.DogImageId;
                    break;
                default:
                    return false;
            }

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateDogInfoRemove(int id, string mark)
        {
            switch (mark)
            {
                case "Food":
                    DogInfo dogInfoFood =
                        await
                        _context
                        .DogInfos
                        .SingleAsync(a => a.FoodId == id);
                    dogInfoFood.FoodId = 0;
                    return await _context.SaveChangesAsync() == 1;
                case "Special":
                    DogInfo dogInfoSpecial =
                        await
                        _context
                        .DogInfos
                        .SingleAsync(a => a.SpecialId == id);
                    dogInfoSpecial.SpecialId = 0;
                    return await _context.SaveChangesAsync() == 1;
                case "Vet":
                    DogInfo dogInfoVet =
                        await
                        _context
                        .DogInfos
                        .SingleAsync(a => a.VetId == id);
                    dogInfoVet.VetId = 0;
                    return await _context.SaveChangesAsync() == 1;
                case "DogImage":
                    DogInfo dogInfoImage =
                        await
                        _context
                        .DogInfos
                        .SingleAsync(a => a.DogImageId == id);
                    dogInfoImage.DogImageId = 0;
                    return await _context.SaveChangesAsync() == 1;
                default:
                    break;
            }

            return await _context.SaveChangesAsync() == 1;
        }
        public async Task<bool> DeleteDogInfo(int id)
        {
            var dogInfo =
                _context
                .DogInfos
                .Single(e => e.DogInfoId == id);
            //Clean up data base

            _context.DogInfos.Remove(dogInfo);

            return await _context.SaveChangesAsync() == 1;
        }
    }

}
