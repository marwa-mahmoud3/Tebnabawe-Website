using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.NewsT
{
    public class NewsRepository : BaseRepository<News>
    {
        private DbContext EC_DbContext;

        public NewsRepository(DbContext EC_DbContext) : base(EC_DbContext)
        {
            this.EC_DbContext = EC_DbContext;
        }
        #region CRUB

        public IEnumerable<News> GetAllNews()
        {
            return GetAll().ToList();
        }

        public bool InsertNews(News news)
        {
            return Insert(news);
        }
        public void UpdateNews(News news)
        {
            Update(news);
        }
        public News GetNewsById(int id)
        {
            var news = DbSet.FirstOrDefault(a => a.Id == id);
            return news;
        }
        #endregion
    }
}
