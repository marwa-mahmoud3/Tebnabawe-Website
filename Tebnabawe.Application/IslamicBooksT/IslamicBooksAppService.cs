using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Application.IslamicBooksT.Dto;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.IslamicBooksT
{
    public class IslamicBooksAppService : AppServiceBase
    {
        public IslamicBooksAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<IslamicBooksDto> GetAll()
        {
            IEnumerable<IslamicBooks> allAbouts = TheUnitOfWork.IslamicBooks.GetAllIslamicBooks();
            return Mapper.Map<IEnumerable<IslamicBooksDto>>(allAbouts);
        }
        public IslamicBooksDto GetIslamicBooksById(int id)
        {
            return Mapper.Map<IslamicBooksDto>(TheUnitOfWork.IslamicBooks.GetById(id));
        }
        public bool Save(IslamicBooksDto islamicBooksDto)
        {
            if (islamicBooksDto == null)
                throw new ArgumentNullException();
            bool result = false;
            var islamicBooks = Mapper.Map<IslamicBooks>(islamicBooksDto);
            if (TheUnitOfWork.IslamicBooks.Insert(islamicBooks))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(IslamicBooksDto islamicBooksDto)
        {
            var islamicBooks = TheUnitOfWork.IslamicBooks.GetById(islamicBooksDto.Id);
            Mapper.Map(islamicBooksDto, islamicBooks);
            TheUnitOfWork.IslamicBooks.Update(islamicBooks);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;
                
            TheUnitOfWork.IslamicBooks.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public IEnumerable<IslamicBooksDto> GetBooksByPagination(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            var books = TheUnitOfWork.IslamicBooks.GetWhere(p => p.Id > 0)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return Mapper.Map<List<IslamicBooksDto>>(books);
        }
        public int BooksCount()
        {
            return TheUnitOfWork.IslamicBooks.CountEntity();
        }
    }
}