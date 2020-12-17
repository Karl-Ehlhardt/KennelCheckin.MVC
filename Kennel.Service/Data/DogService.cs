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
    class DogService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public DogService(Guid userId)
        {
            _userId = userId;
        }

        //Create new dogBasic
        public async Task<bool> CreateDogBasic(DogBasicCreate model)
        {
            DogBasic dogBasic =
                new DogBasic()
                {
                    DogName = model.DogName,
                    Breed = model.Breed,
                    Weight = model.Weight,
                    Size = model.Size
                };

            _context.DogBasics.Add(dogBasic);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get area by id
        public async Task<List<DogBasicDetails>> GetAreaById([FromUri] int id)
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
    }
}
