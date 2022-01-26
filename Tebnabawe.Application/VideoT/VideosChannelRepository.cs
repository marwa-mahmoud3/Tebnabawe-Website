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
    public class VideosChannelRepository : BaseRepository<VideosChannel>
    {
        private DbContext EC_DbContext;

        public VideosChannelRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<VideosChannel> GetAllVideos()
        {
            return GetAll().ToList();
        }

        public bool InsertVideo(VideosChannel videosLibrary)
        {
            return Insert(videosLibrary);
        }
        public void UpdateVideo(VideosChannel videosLibrary)
        {
            Update(videosLibrary);
        }
        public VideosChannel GetVideoById(int id)
        {
            var video = DbSet.FirstOrDefault(a => a.Id == id);
            return video;
        }
        #endregion
    }
}
