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
        public async Task<bool> CreateDogInfo()
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
            return await _context.SaveChangesAsync() == 1;
        }

        //Create new dogInfo
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

            DogInfoDetails model = new DogInfoDetails(dogBasic);

            return model;
        }

        //Update dogInfo by id
        public async Task<bool> UpdateDogInfo([FromUri] int id, [FromBody] DogInfoEdit model)
        {
            DogInfo dogInfo =
                _context
                .DogInfos
                .Single(a => a.DogInfoId == id);
            dogInfo.FoodId = model.FoodId;
            dogInfo.SpecialId = model.SpecialId;
            dogInfo.VetId = model.VetId;
            dogInfo.MedicationIdList = model.MedicationIdList;

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
