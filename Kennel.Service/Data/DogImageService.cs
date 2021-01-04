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


            //ImageConverter _imageConverter = new ImageConverter();
            //byte[] xByte = (byte[])_imageConverter.ConvertTo(file, typeof(byte[]));

            DogImage dogImage =
                new DogImage()
                {
                    ImgFile = image
                };

            _context.DogImages.Add(dogImage);
            return await _context.SaveChangesAsync() == 1;
        }

        //Get by id
        public async Task<DogImageDisplay> GetDogImageById([FromUri] int id)
        {
            var query =
                await
                _context
                .DogImages
                .SingleAsync(q => q.DogImageId == id);

            MemoryStream bipimag = new MemoryStream(query.ImgFile);
            Image imag = new Bitmap(bipimag);

            //ImageConverter _imageConverter = new ImageConverter();
            //Image image = (Image)_imageConverter.ConvertTo(query.ImgFile, typeof(Image));

            DogImageDisplay dogImageDisplay =
                new DogImageDisplay()
                {
                    DogImageId = query.DogImageId,
                    ImgOut = imag
                };

            return dogImageDisplay;
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
