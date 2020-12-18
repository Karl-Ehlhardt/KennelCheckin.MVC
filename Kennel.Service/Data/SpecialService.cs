using Kennel.Data.Users;
using Kennel.Models.Data.Special;
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
    public class SpecialService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public SpecialService(Guid userId)
        {
            _userId = userId;
        }

        //Create new special
        public async Task<bool> CreateSpecial(SpecialCreate model)
        {
            Special special =
                new Special()
                {
                    Instructions = model.Instructions,
                    Allergies = model.Allergies
                };

            _context.Specials.Add(special);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get special by id
        public async Task<SpecialDetails> GetSpecialById([FromUri] int id)
        {
            var query =
                await
                _context
                .Specials
                .Where(q => q.SpecialId == id)
                .Select(
                    q =>
                    new SpecialDetails()
                    {
                        Instructions = q.Instructions,
                        Allergies = q.Allergies
                    }).ToListAsync();
            return query[0];
        }

        //Update by id
        public async Task<bool> UpdateSpecial([FromUri] int id, [FromBody] SpecialEdit model)
        {
            Special special =
                _context
                .Specials
                .Single(a => a.SpecialId == id);
            special.Instructions = model.Instructions;
            special.Allergies = model.Allergies;

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
