using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tebnabawe.Application.Bases;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.WorksT
{
    public class WorkRepository : BaseRepository<Works>
    {
        private DbContext EC_DbContext;

        public WorkRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<Works> GetAllWorks()
        {
            return GetAll()
                .ToList();
        }

        public bool InsertWork(Works work)
        {
            return Insert(work);
        }

        public void UpdateWork(Works work)
        {
            Update(work);
        }
        public void DeleteWork(int id)
        {
            Delete(id);
        }
        public Works GetWorkById(int id)
        {
            var work = DbSet
                .FirstOrDefault(w => w.Id == id);
            return work;
        }
        public IEnumerable<Works> GetWorksByAbout(int aboutId)
        {
            var query = DbSet
                .Where(w => w.AboutId == aboutId);
            return query;
        }

        #endregion
    }
}