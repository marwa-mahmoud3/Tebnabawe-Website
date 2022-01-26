using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.PhotosLibraryT
{
   public class PhotosLibraryRepository: BaseRepository<PhotosLibrary>
    {
        private DbContext EC_DbContext;

        public PhotosLibraryRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<PhotosLibrary> GetAllPhotos()
        {
            return GetAll().ToList();
        }

        public bool InsertPhoto(PhotosLibrary photo)
        {
            return Insert(photo);
        }
        public void UpdatePhoto(PhotosLibrary photo)
        {
            Update(photo);
        }
        public PhotosLibrary GetPhotoById(int id)
        {
            var photo = DbSet.FirstOrDefault(a => a.Id == id);
            return photo;
        }
        #endregion
    }
}