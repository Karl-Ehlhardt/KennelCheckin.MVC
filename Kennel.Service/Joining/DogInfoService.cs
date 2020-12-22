using Kennel.Data.Users;
using Kennel.Models.Data.DisplayOnly;
using Kennel.Models.Joining_Data.DogInfo;
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

            foreach (DogInfo dogIn in dogInfo)
            {
                DogBasic item =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogIn.DogBasicId);
                dogBasic.Add(item);
            }

            DogInfoIndexView model = new DogInfoIndexView(dogBasic.AsEnumerable(), owner, dogInfo);

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

            DogInfoDetails model = new DogInfoDetails(dogInfo, dogBasic, food);

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
                case "Medication":
                    Medication medication =
                        await
                    _context
                    .Medications.OrderByDescending(p => p.MedicationId)
                    .FirstOrDefaultAsync();
                    dogInfo.MedicationIdList.Add(medication.MedicationId);
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
                case "Medication":
                    foreach (DogInfo dogInfo in _context.DogInfos)
                    {
                        for (int i = 0; i < dogInfo.MedicationIdList.Count(); ++i)
                        {
                            if (dogInfo.MedicationIdList[i] == id)
                            {
                                dogInfo.MedicationIdList.RemoveAt(i);
                                return await _context.SaveChangesAsync() == 1;
                            }
                        }
                    }
                    break;
                default:
                    break;
            }

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
