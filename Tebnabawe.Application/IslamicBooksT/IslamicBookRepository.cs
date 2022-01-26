using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.IslamicBooksT
{
    public class IslamicBookRepository : BaseRepository<IslamicBooks>
    {
        private DbContext EC_DbContext;

        public IslamicBookRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<IslamicBooks> GetAllIslamicBooks()
        {
            return GetAll().ToList();
        }

        public bool InsertIslamicBooks(IslamicBooks islamicBooks)
        {
            return Insert(islamicBooks);
        }
        public void UpdateIslamicBooks(IslamicBooks islamicBooks)
        {
            Update(islamicBooks);
        }
        public IslamicBooks GetIslamicBooksById(int id)
        {
            var islamicBooks = DbSet.FirstOrDefault(a => a.Id == id);
            return islamicBooks;
        }
        #endregion
    }
}
