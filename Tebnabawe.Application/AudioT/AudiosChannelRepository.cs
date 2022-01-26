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
    public class AudiosChannelRepository : BaseRepository<AudiosChannel>
    {
        private DbContext EC_DbContext;

        public AudiosChannelRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<AudiosChannel> GetAllAudios()
        {
            return GetAll().ToList();
        }

        public bool InsertAudio(AudiosChannel audiosChannel)
        {
            return Insert(audiosChannel);
        }
        public void UpdateAudio(AudiosChannel audiosChannel)
        {
            Update(audiosChannel);
        }
        public AudiosChannel GetAudioById(int id)
        {
            var audio = DbSet.FirstOrDefault(a => a.Id == id);
            return audio;
        }
        #endregion
    }
}
