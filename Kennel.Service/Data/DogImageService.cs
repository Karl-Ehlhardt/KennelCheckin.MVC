using Kennel.Data.Users;
using Kennel.Models.Data.DogImage;
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
    class DogImageService
    {
        //private user field
        private readonly Guid _userId;

        //private context
        private ApplicationDbContext _context = new ApplicationDbContext();

        //service constructor
        public DogImageService(Guid userId)
        {
            _userId = userId;
        }

        //Create new
        public async Task<bool> CreateDogImage(DogImageCreate model)
        {
            DogImage dogImage =
                new DogImage()
                {
                    ImgFile = model.ImgFile,
                };

            _context.DogImages.Add(dogImage);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get by id
        public async Task<List<DogImageDisplay>> GetDogImageById([FromUri] int id)
        {
            var query =
                await
                _context
                .DogImages
                .Where(q => q.DogImageId == id)
                .Select(
                    q =>
                    new DogImageDisplay()
                    {
                        ImgFile = q.ImgFile,
                    }).ToListAsync();
            return query;
        }

        //Update by id
        public async Task<bool> UpdateDogImage([FromUri] int id, [FromBody] DogImageEdit model)
        {
            DogImage dogImage =
                _context
                .DogImages
                .Single(a => a.DogImageId == id);
            dogImage.ImgFile = model.ImgFile;

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
