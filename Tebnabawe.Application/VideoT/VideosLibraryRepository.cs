using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.VideoT
{
    public class VideosLibraryRepository : BaseRepository<VideosLibrary>
    {
        private DbContext EC_DbContext;

        public VideosLibraryRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<VideosLibrary> GetAllVideos()
        {
            return GetAll().ToList();
        }

        public bool InsertVideo(VideosLibrary videosLibrary)
        {
            return Insert(videosLibrary);
        }
        public void UpdateVideo(VideosLibrary videosLibrary)
        {
            Update(videosLibrary);
        }
        public VideosLibrary GetVideoById(int id)
        {
            var video = DbSet.FirstOrDefault(a => a.Id == id);
            return video;
        }
        #endregion
    }
}
