using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Tebnabawe.Application.Bases;
using Tebnabawe.Application.GoalsT.Dto;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.GoalsT
{
    public class GoalsAppService: AppServiceBase
    {
        public GoalsAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<GoalsModel> GetAll(int AboutId)
        {
            IEnumerable<Goals> res = TheUnitOfWork.Goals.GetGoalsByAbout(AboutId);
            return Mapper.Map<IEnumerable<GoalsModel>>(res);
        }
        public GoalsModel GetById(int id)
        {
            return Mapper.Map<GoalsModel>(TheUnitOfWork.Goals.GetById(id));
        }
        public bool Save(GoalsModel goalsModel)
        {
            if (goalsModel == null)
                throw new ArgumentNullException();
            bool result = false;
            var goal = Mapper.Map<Goals>(goalsModel);
            if (TheUnitOfWork.Goals.Insert(goal))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(GoalsModel goalsModel)
        {
            var goalFromDB = TheUnitOfWork.Goals.GetById(goalsModel.Id);
            Mapper.Map(goalsModel, goalFromDB);
            TheUnitOfWork.Goals.Update(goalFromDB);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.Goals.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public IEnumerable<GoalsModel> GetGoalsByPagination(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            var goals = TheUnitOfWork.Goals.GetWhere(g=>g.AboutId==1)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList(); 

            return Mapper.Map<List<GoalsModel>>(goals);
        }
        public int GoalsCount()
        {
            return TheUnitOfWork.Goals.CountEntity();
        }
    }
}