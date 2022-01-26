using AutoMapper;
using System;
using System.Collections.Generic;
using Tebnabawe.Application.AboutT.Dto;
using Tebnabawe.Application.Bases;
using Tebnabawe.Bases;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.AboutT
{
    public class AboutAppService : AppServiceBase
    {
        public AboutAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {
            
        }
        public IEnumerable<AboutModel> GetAll()
        {
            IEnumerable<About> allAbouts = TheUnitOfWork.About.GetAllAbout();
            return Mapper.Map<IEnumerable<AboutModel>>(allAbouts);
        }
        public AboutModel GetAboutById(int id)
        {
            return Mapper.Map<AboutModel>(TheUnitOfWork.About.GetAboutById(id));
        }
        public AboutModel GetAboutByAdminId(string id)
        {
            return Mapper.Map<AboutModel>(TheUnitOfWork.About.GetAboutByAdminId(id));
        }
        public bool Save(AboutModel aboutModel)
        {
            if (aboutModel == null)
                throw new ArgumentNullException();
            bool result = false;
            var about = Mapper.Map<About>(aboutModel);
            if (TheUnitOfWork.About.Insert(about))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(AboutModel aboutModel)
        {
            var aboutFromDB = TheUnitOfWork.About.GetById(aboutModel.Id);
            Mapper.Map(aboutModel, aboutFromDB);
            TheUnitOfWork.About.Update(aboutFromDB);
            TheUnitOfWork.Commit();
            return true;
        }
    }
}
