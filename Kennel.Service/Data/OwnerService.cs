using Kennel.Data.Users;
using Kennel.Models.Data.Owner;
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
    public class OwnerService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public OwnerService(Guid userId)
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

        //Create new
        public async Task<bool> CreateOwner(OwnerCreate model)
        {
            Owner owner =
                new Owner()
                {
                    ApplicationUserId = _userId.ToString(),
                    Name = model.Name,
                    Phone = model.Phone,
                    Email = model.Email,
                    BackupName = model.BackupName,
                    BackupPhone = model.BackupPhone,
                    BackupEmail = model.BackupEmail,
                };

            _context.Owners.Add(owner);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get by id
        public async Task<OwnerDetails> GetOwnerById([FromUri] int id)
        {
            var query =
                await
                _context
                .Owners
                .Where(q => q.OwnerId == id)
                .Select(
                    q =>
                    new OwnerDetails()
                    {
                        OwnerId = q.OwnerId,
                        Name = q.Name,
                        Phone = q.Phone,
                        Email = q.Email,
                        BackupName = q.BackupName,
                        BackupPhone = q.BackupPhone,
                        BackupEmail = q.BackupEmail
                    }).ToListAsync();
            return query[0];
        }

        //Get by id
        public async Task<OwnerEdit> GetOwnerByIdEditable([FromUri] int id)
        {
            var query =
                await
                _context
                .Owners
                .Where(q => q.OwnerId == id)
                .Select(
                    q =>
                    new OwnerEdit()
                    {
                        Name = q.Name,
                        Phone = q.Phone,
                        Email = q.Email,
                        BackupName = q.BackupName,
                        BackupPhone = q.BackupPhone,
                        BackupEmail = q.BackupEmail
                    }).ToListAsync();
            return query[0];
        }

        //Update by id
        public async Task<bool> UpdateOwner([FromUri] int id, [FromBody] OwnerEdit model)
        {
            Owner owner =
                _context
                .Owners
                .Single(a => a.OwnerId == id);
            owner.Name = model.Name;
            owner.Phone = model.Phone;
            owner.Email = model.Email;
            owner.BackupName = model.BackupName;
            owner.BackupPhone = model.BackupPhone;
            owner.BackupEmail = model.BackupEmail;

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
