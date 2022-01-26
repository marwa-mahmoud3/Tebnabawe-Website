using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using Tebnabawe.Application.Bases;
using Tebnabawe.Application.WorksT.Dto;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.WorkT
{
    public class WorkAppService : AppServiceBase
    {
        public WorkAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<WorkModel> GetAll(int AboutId)
        {
            IEnumerable<Works> res = TheUnitOfWork.Work.GetWorksByAbout(AboutId);
            return Mapper.Map<IEnumerable<WorkModel>>(res);
        }
        public WorkModel GetById(int id)
        {
            return Mapper.Map<WorkModel>(TheUnitOfWork.Work.GetById(id));
        }
        public bool Save(WorkModel workModel)
        {
            if (workModel == null)
                throw new ArgumentNullException();
            bool result = false;
            var work = Mapper.Map<Works>(workModel);
            if (TheUnitOfWork.Work.InsertWork(work))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(WorkModel workModel)
        {
            var workFromDB = TheUnitOfWork.Work.GetById(workModel.Id);
            Mapper.Map(workModel, workFromDB); 
            TheUnitOfWork.Work.Update(workFromDB);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.Work.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public IEnumerable<WorkModel> GetWorksByPagination(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            var works = TheUnitOfWork.Work.GetWhere(w => w.AboutId == 1)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return Mapper.Map<List<WorkModel>>(works);
        }
        public int WorksCount()
        {
            return TheUnitOfWork.Work.CountEntity();
        }
    }
}