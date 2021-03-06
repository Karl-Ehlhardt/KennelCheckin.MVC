﻿using Kennel.Data.Users;
using Kennel.Models.Data.DogBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KennelData.Data;
using System.Web.Http;
using System.Data.Entity;

namespace Kennel.Service.Data
{
    public class DogBasicService
    {

        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public DogBasicService(Guid userId)
        {
            _userId = userId;
        }

        public async Task<bool> CheckOwner()
        {

            Owner owner =
                await
                _context
                .Owners
                .SingleOrDefaultAsync(a => a.ApplicationUserId == _userId.ToString());

            return owner == null;
        }

        //Create new dogBasic
        public async Task<bool> CreateDogBasic(DogBasicCreate model)
        {
            DogBasic dogBasic =
                new DogBasic()
                {
                    DogName = model.DogName,
                    Breed = model.Breed,
                    Weight = model.Weight
                };

            _context.DogBasics.Add(dogBasic);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get dogBasic by id
        //public async Task<DogBasicDetails> GetDogBasicById([FromUri] int id)
        //{
        //    var query =
        //        await
        //        _context
        //        .DogBasics
        //        .Where(q => q.DogBasicId == id)
        //        .Select(
        //            q =>
        //            new DogBasicDetails()
        //            {
        //                DogBasicId = q.DogBasicId,
        //                DogName = q.DogName,
        //                Breed = q.Breed,
        //                Weight = q.Weight
        //            }).ToListAsync();
        //    return query[0];
        //}

        //Get dogBasic by id Editable
        public async Task<DogBasicEdit> GetDogBasicByIdEditable([FromUri] int id)
        {
            var query =
                await
                _context
                .DogBasics
                .Where(q => q.DogBasicId == id)
                .Select(
                    q =>
                    new DogBasicEdit()
                    {
                        DogName = q.DogName,
                        Breed = q.Breed,
                        Weight = q.Weight
                    }).ToListAsync();
            return query[0];
        }

        //Update area by id
        public async Task<bool> UpdateDogBasic([FromUri] int id, [FromBody] DogBasicEdit model)
        {
            DogBasic dogBasic =
                _context
                .DogBasics
                .Single(a => a.DogBasicId == id);
            dogBasic.DogName = model.DogName;
            dogBasic.Breed = model.Breed;
            dogBasic.Weight = model.Weight;

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
