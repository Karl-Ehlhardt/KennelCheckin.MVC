using Kennel.Data.Users;
using Kennel.Models.Joining_Data.DogVisit;
using KennelData.JoiningData;
using System;
using System.Collections.Generic;
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
        public async Task<List<DogVisitListItem>> GetAllDogVisit()
        {
            var query =
                await
                _context
                .DogBasics
                .Where(q => q.DogBasicId == id)
                .Select(
                    q =>
                    new DogBasicDetails()
                    {
                        DogName = q.DogName,
                        Breed = q.Breed,
                        Weight = q.Weight,
                        Size = q.Size
                    }).ToListAsync();
            return query;
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
    }
}
