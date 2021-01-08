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
        public async Task<bool> CreateDogVisit(int id, DogVisitCreate model)
        {
            DogVisit dogVisit =
                new DogVisit()
                {
                    DogInfoId = id,
                    DropOffTime = model.DropOffTime,
                    PickUpTime = model.PickUpTime,
                    Notes = model.Notes,
                    OnSite = false
                };

            _context.DogVisits.Add(dogVisit);
            return await _context.SaveChangesAsync() == 1;
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
                        DropOffTime = q.DropOffTime,
                        PickUpTime = q.PickUpTime,
                        Notes = q.Notes
                    }).ToListAsync();

            query[0].DogName = await GetDogName(id); //uses the DogVisit id to do a couple look ups and returns the dogs name

            return query[0];
        }

        public async Task<DogVisitEdit> GetDogVisitByIdEditable(int id)
        {
            var query =
                await
                _context
                .DogVisits
                .Where(q => q.DogVisitId == id)
                .Select(
                    q =>
                    new DogVisitEdit()
                    {
                        DropOffTime = q.DropOffTime,
                        PickUpTime = q.PickUpTime,
                        Notes = q.Notes
                    }).ToListAsync();
            query[0].DogName = await GetDogName(id); //uses the DogVisit id to do a couple look ups and returns the dogs name
            return query[0];
        }
        
        public async Task<DogVisitChangePickup> GetDogVisitByIdChangePickup(int id)
        {
            var query =
                await
                _context
                .DogVisits
                .Where(q => q.DogVisitId == id)
                .Select(
                    q =>
                    new DogVisitChangePickup()
                    {
                        PickUpTime = q.PickUpTime,
                    }).ToListAsync();
            query[0].DogName = await GetDogName(id); //uses the DogVisit id to do a couple look ups and returns the dogs name
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

        public async Task<bool> ChangePickupDogVisit([FromUri] int id, [FromBody] DogVisitChangePickup model)
        {

            DogVisit dogVisit =
                _context
                .DogVisits
                .Single(a => a.DogVisitId == id);
            dogVisit.PickUpTime = model.PickUpTime;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteDogVisit(int id)
        {
            var entity =
                _context
                .DogVisits
                .Single(e => e.DogVisitId == id);

            _context.DogVisits.Remove(entity);

            return await _context.SaveChangesAsync() == 1;
        }

        //================Helpers========================
        //Helper the get dog name
        public async Task<string> GetDogName(int dogVisitid)
        {
            DogVisit dogVisit =
                await
                _context
                .DogVisits
                .SingleAsync(a => a.DogVisitId == dogVisitid);

            DogInfo dogInfo =
                await
                _context
                .DogInfos
                .SingleAsync(a => a.DogInfoId == dogVisit.DogInfoId);

            DogBasic dogBasic =
                await
                _context
                .DogBasics
                .SingleAsync(q => q.DogBasicId == dogInfo.DogBasicId);

            return dogBasic.DogName;
        }

    }
}
