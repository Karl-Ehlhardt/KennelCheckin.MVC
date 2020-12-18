using Kennel.Data.Users;
using Kennel.Models.Joining_Data.DogInfo;
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

        //Create new dogInfo
        public async Task<bool> CreateDogInfo(DogInfoCreate model)
        {
            DogInfo dogInfo =
                new DogInfo()
                {
                    DogBasicId = model.DogBasicId,
                    OwnerId = model.OwnerId
                };

            _context.DogInfos.Add(dogInfo);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get dogInfo by id not needed
        public async Task<DogInfoEdit> GetDogInfoById(int id)
        {
            var query =
                await
                _context
                .DogInfos
                .Where(q => q.DogInfoId == id)
                .Select(
                    q =>
                    new DogInfoEdit()
                    {
                        FoodId = q.FoodId,
                        MedicationIdList = q.MedicationIdList,
                        SpecialId = q.SpecialId,
                        VetId = q.VetId
                    }).ToListAsync();

            return query[0];
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
