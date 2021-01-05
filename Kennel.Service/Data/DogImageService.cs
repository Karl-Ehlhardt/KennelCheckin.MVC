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
using System.Drawing;
using System.Web;
using System.IO;

namespace Kennel.Service.Data
{
    public class DogImageService
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
        public async Task<bool> CreateDogImage(HttpPostedFileBase file)
        {
            byte[] image = new byte[file.ContentLength];
            file.InputStream.Position = 0;
            file.InputStream.Read(image, 0, image.Length);

            DogImage dogImage =
                new DogImage()
                {
                    ImgFile = image
                };

            _context.DogImages.Add(dogImage);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get by id
        public async Task<DogImageEdit> GetDogImageIdEdit([FromUri] int id)
        {
            var query =
                await
                _context
                .DogImages
                .SingleAsync(q => q.DogImageId == id);

            DogImageEdit dogImageEdit =
                new DogImageEdit()
                {
                    DogImageId = query.DogImageId,
                };

            return dogImageEdit;
        }

        //Update by id
        public async Task<bool> UpdateDogImage([FromUri] int id, [FromBody] HttpPostedFileBase file)
        {

            byte[] image = new byte[file.ContentLength];
            file.InputStream.Position = 0;
            file.InputStream.Read(image, 0, image.Length);

            DogImage dogImage =
                _context
                .DogImages
                .Single(a => a.DogImageId == id);
            dogImage.ImgFile = image;

            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteDogImage(int id)
        {
            var entity =
                _context
                .DogImages
                .Single(e => e.DogImageId == id);

            _context.DogImages.Remove(entity);

            return await _context.SaveChangesAsync() == 1;
        }
    }
}
