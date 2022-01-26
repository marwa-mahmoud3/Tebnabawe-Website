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
    public class RadioDataRepositiory : BaseRepository<RadioData>
    {
        private DbContext EC_DbContext;

        public RadioDataRepositiory(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<RadioData> GetAllRadiosData()
        {
            return GetAll().ToList();
        }

        public bool InsertRadioData(RadioData radioData)
        {
            return Insert(radioData);
        }
        public void UpdateRadioData(RadioData radioData)
        {
            Update(radioData);
        }
        public RadioData GetRadioDataById(int id)
        {
            var radiodata = DbSet.FirstOrDefault(a => a.Id == id);
            return radiodata;
        }
        #endregion
    }
}
