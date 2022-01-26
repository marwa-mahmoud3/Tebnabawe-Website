using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Application.NewsT.Dto;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.NewsT
{
    public class NewsAppService : AppServiceBase
    {
        public NewsAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<NewsDto> GetAll()
        {
            IEnumerable<News> allAbouts = TheUnitOfWork.News.GetAllNews();
            return Mapper.Map<IEnumerable<NewsDto>>(allAbouts);
        }
        public NewsDto GetNewsById(int id)
        {
            return Mapper.Map<NewsDto>(TheUnitOfWork.News.GetNewsById(id));
        }
        public bool Save(NewsDto newsDto)
        {
            if (newsDto == null)
                throw new ArgumentNullException();
            bool result = false;
            var news = Mapper.Map<News>(newsDto);
            if (TheUnitOfWork.News.Insert(news))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(NewsDto newsDto)
        {
            var news = TheUnitOfWork.News.GetById(newsDto.Id);
            Mapper.Map(newsDto, news);
            TheUnitOfWork.News.Update(news);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.News.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public IEnumerable<NewsDto> GetNewsByPagination(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            var news = TheUnitOfWork.News.GetWhere(p => p.Id > 0)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return Mapper.Map<List<NewsDto>>(news);
        }
        public int NewsCount()
        {
            return TheUnitOfWork.News.CountEntity();
        }
    }
}
