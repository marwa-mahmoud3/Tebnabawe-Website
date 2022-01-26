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
    public class RequestRadioRepository : BaseRepository<RequestRadioPage>
    {
        private DbContext EC_DbContext;

        public RequestRadioRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<RequestRadioPage> GetAllRequests()
        {
            return GetAll().ToList();
        }

        public bool InsertRequest(RequestRadioPage R)
        {
            return Insert(R);
        }
        public void UpdateRequest(RequestRadioPage r)
        {
            Update(r);
        }
        public RequestRadioPage GetRequestById(int id)
        {
            var audio = DbSet.FirstOrDefault(a => a.Id == id);
            return audio;
        }
        #endregion
    }
}

