using Kennel.Data.Users;
using Kennel.Models.Joining_Data.DogVisit;
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
    public class DogVisitService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public DogVisitService(Guid userId)
        {
            _userId = userId;
        }

        //Create new dogVisit
        public async Task<bool> CreateDogVisit(DogVisitCreate model)
        {
            DogVisit dogVisit =
                new DogVisit()
                {
                    DogInfoId = model.DogInfoId,
                    DropOffTime = model.DropOffTime,
                    PickUpTime = model.PickUpTime,
                    Notes = model.Notes
                };

            _context.DogVisits.Add(dogVisit);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get dogVisit by id not needed
        public async Task<List<DogVisitListItem>> GetAllDogVisits()
        {
            var query =
                await
                _context
                .DogVisits
                .Select(
                    q =>
                    new DogVisitListItem()
                    {
                        DogName = GetDogName(q.DogInfoId),
                        DropOffTime = q.DropOffTime,
                        PickUpTime = q.PickUpTime,
                        Notes = q.Notes
                    }).ToListAsync();
            return query;
        }

        public async Task<DogVisitListItem> GetDogVisitById(int id)
        {
            var query =
                await
                _context
                .DogVisits
                .Where(q => q.DogVisitId == id)
                .Select(
                    q =>
                    new DogVisitListItem()
                    {
                        DogName = GetDogName(q.DogInfoId),
                        DropOffTime = q.DropOffTime,
                        PickUpTime = q.PickUpTime,
                        Notes = q.Notes
                    }).ToListAsync();
            return query[0];
        }


        //Update dogVisit by id
        public async Task<bool> UpdateDogVisit([FromUri] int id, [FromBody] DogVisitEdit model)
        {
            DogVisit dogVisit =
                _context
                .DogVisits
                .Single(a => a.DogVisitId == id);
            dogVisit.DropOffTime = model.DropOffTime;
            dogVisit.PickUpTime = model.PickUpTime;
            dogVisit.Notes = model.Notes;

            return await _context.SaveChangesAsync() == 1;
        }


        //================Helpers========================
        //Helper the get dog name
        public string GetDogName(int id)
        {
            DogInfo dogInfo =
                _context
                .DogInfos
                .Single(a => a.DogInfoId == id);

            DogBasic dogBasic =
                _context
                .DogBasics
                .Single(q => q.DogBasicId == dogInfo.DogBasicId);

            return dogBasic.DogName;
        }
    }
}
