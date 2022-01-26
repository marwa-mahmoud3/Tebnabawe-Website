using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.BrochuresT
{
    public class BrochuresRepository : BaseRepository<Brochures>
    {
        private DbContext EC_DbContext;

        public BrochuresRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<Brochures> GetAllBrochures()
        {
            return GetAll().ToList();
        }

        public bool InsertBrochures(Brochures brochures)
        {
            return Insert(brochures);
        }
        public void UpdateBrochures(Brochures brochures)
        {
            Update(brochures);
        }
        public Brochures GetBrochuresById(int id)
        {
            var brochures = DbSet.FirstOrDefault(a => a.Id == id);
            return brochures;
        }
        #endregion
    }
}
