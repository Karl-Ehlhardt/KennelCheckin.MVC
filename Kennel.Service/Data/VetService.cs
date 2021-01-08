using Kennel.Data.Users;
using Kennel.Models.Data.Vet;
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
    public class VetService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public VetService(Guid userId)
        {
            _userId = userId;
        }

        //Create new vet
        public async Task<bool> CreateVet(VetCreate model)
        {
            Vet vet =
                new Vet()
                {
                    BusinessName = model.BusinessName,
                    VetName = model.VetName,
                    Phone = model.Phone
                };

            _context.Vets.Add(vet);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get vet by id
        public async Task<VetDetails> GetVetById([FromUri] int id)
        {
            var query =
                await
                _context
                .Vets
                .Where(q => q.VetId == id)
                .Select(
                    q =>
                    new VetDetails()
                    {
                        BusinessName = q.BusinessName,
                        VetName = q.VetName,
                        Phone = q.Phone
                    }).ToListAsync();
            return query[0];
        }

        //Get vet by id
        public async Task<VetEdit> GetVetByIdEditable([FromUri] int id)
        {
            var query =
                await
                _context
                .Vets
                .Where(q => q.VetId == id)
                .Select(
                    q =>
                    new VetEdit()
                    {
                        BusinessName = q.BusinessName,
                        VetName = q.VetName,
                        Phone = q.Phone
                    }).ToListAsync();
            return query[0];
        }

        //Update area by id
        public async Task<bool> UpdateVet([FromUri] int id, [FromBody] VetEdit model)
        {
            Vet vet =
                _context
                .Vets
                .Single(a => a.VetId == id);
            vet.BusinessName = model.BusinessName;
            vet.VetName = model.VetName;
            vet.Phone = model.Phone;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteVet(int id)
        {
            var entity =
                _context
                .Vets
                .Single(e => e.VetId == id);

            _context.Vets.Remove(entity);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
