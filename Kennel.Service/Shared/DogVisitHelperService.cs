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

namespace Kennel.Service.Shared
{
    public class DogVisitHelperService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public DogVisitHelperService(Guid userId)
        {
            _userId = userId;
        }

        //Get
        public async Task<List<DogVisitListItem>> GetAllFutureDogVisits(int dogInfoId, string dogName)
        {
            var query =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == false && q.DogInfoId == dogInfoId)
                .Select(
                    q =>
                    new DogVisitListItem()
                    {
                        DogVisitId = q.DogVisitId,
                        DogName = dogName ,
                        DropOffTime = q.DropOffTime,
                        PickUpTime = q.PickUpTime,
                        Notes = q.Notes
                    }).ToListAsync();

            return query;
        }

        //Get
        public async Task<List<DogVisitListItem>> GetAllOngoingDogVisits(int dogInfoId, string dogName)
        {
            var query =
                await
                _context
                .DogVisits
                .Where(q => q.OnSite == true && q.DogInfoId == dogInfoId)
                .Select(
                    q =>
                    new DogVisitListItem()
                    {
                        DogVisitId = q.DogVisitId,
                        DogName = dogName,
                        DropOffTime = q.DropOffTime,
                        PickUpTime = q.PickUpTime,
                        Notes = q.Notes
                    }).ToListAsync();
            return query;
        }

        //================Helpers========================
        //Helper the get dog name NOT WORKING
        //public string GetDogName(int id)
        //{
        //    DogInfo dogInfo =
        //        _context
        //        .DogInfos
        //        .Single(a => a.DogInfoId == id);

        //    DogBasic dogBasic =
        //        _context
        //        .DogBasics
        //        .Single(q => q.DogBasicId == dogInfo.DogBasicId);

        //    return dogBasic.DogName;
        //}
    }
}
