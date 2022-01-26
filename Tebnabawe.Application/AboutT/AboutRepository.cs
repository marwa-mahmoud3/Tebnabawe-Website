
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Tebnabawe.Application.Bases;
using Tebnabawe.Bases;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.AboutT
{
    public class AboutRepository : BaseRepository<About>
    {
        private DbContext EC_DbContext;

        public AboutRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<About> GetAllAbout()
        {
            return GetAll()
                .ToList();
        }

        public bool InsertAbout(About about)
        {
            return Insert(about);
        }
        public void UpdateAbout(About about)
        {
            Update(about);
        }
        public About GetAboutById(int id)
        {
            var about = DbSet
                .FirstOrDefault(a => a.Id == id);
            return about;
        }
        public About GetAboutByAdminId(string id)
        {
            var about = DbSet
                .FirstOrDefault(a => a.AdminId == id);
            return about;
        }
        #endregion
    }
}