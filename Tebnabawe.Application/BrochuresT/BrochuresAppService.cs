using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Application.BrochuresT.Dto;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.BrochuresT
{
    public class BrochuresAppService : AppServiceBase
    {
        public BrochuresAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<BrochuresDto> GetAll()
        {
            IEnumerable<Brochures> allAbouts = TheUnitOfWork.Brochures.GetAllBrochures();
            return Mapper.Map<IEnumerable<BrochuresDto>>(allAbouts);
        }
        public BrochuresDto GetBrochureById(int id)
        {
            return Mapper.Map<BrochuresDto>(TheUnitOfWork.Brochures.GetBrochuresById(id));
        }
        public bool Save(BrochuresDto brochuresDto)
        {
            if (brochuresDto == null)
                throw new ArgumentNullException();
            bool result = false;
            var brochures = Mapper.Map<Brochures>(brochuresDto);
            if (TheUnitOfWork.Brochures.Insert(brochures))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(BrochuresDto brochuresDto)
        {
            var brochures = TheUnitOfWork.Brochures.GetById(brochuresDto.Id);
            Mapper.Map(brochuresDto, brochures);
            TheUnitOfWork.Brochures.Update(brochures);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.Brochures.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public IEnumerable<BrochuresDto> GetBrochuresByPagination(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            var Brochures = TheUnitOfWork.Brochures.GetWhere(p => p.Id > 0)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return Mapper.Map<List<BrochuresDto>>(Brochures);
        }
        public int BrochuresCount()
        {
            return TheUnitOfWork.Brochures.CountEntity();
        }
    }
}
