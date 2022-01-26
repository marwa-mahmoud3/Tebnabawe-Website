using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.AudioT
{
    public class AudiosLibraryRepository : BaseRepository<AudiosLibrary>
    {
        private DbContext EC_DbContext;

        public AudiosLibraryRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<AudiosLibrary> GetAllAudios()
        {
            return GetAll().ToList();
        }

        public bool InsertAudio(AudiosLibrary audiosChannel)
        {
            return Insert(audiosChannel);
        }
        public void UpdateAudio(AudiosLibrary audiosChannel)
        {
            Update(audiosChannel);
        }
        public AudiosLibrary GetAudioById(int id)
        {
            var audio = DbSet.FirstOrDefault(a => a.Id == id);
            return audio;
        }
        #endregion
    }
}
