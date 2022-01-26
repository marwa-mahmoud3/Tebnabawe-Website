using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.AudioT.Dto;
using Tebnabawe.Application.Bases;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.AudioT
{
   public class RadioDataService : AppServiceBase
    {
        public RadioDataService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<RadioDataDto> GetAll()
        {
            IEnumerable<RadioData> all = TheUnitOfWork.RadioData.GetAllRadiosData();
            return Mapper.Map<IEnumerable<RadioDataDto>>(all);
        }
        public RadioDataDto GetRadioDataById(int id)
        {
            return Mapper.Map<RadioDataDto>(TheUnitOfWork.RadioData.GetRadioDataById(id));
        }
        public bool Save(RadioDataDto radioData)
        {
            if (radioData == null)
                throw new ArgumentNullException();
            bool result = false;
            var radio  = Mapper.Map<RadioData>(radioData);
            if (TheUnitOfWork.RadioData.Insert(radio))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(RadioDataDto radioData)
        {
            var radio = TheUnitOfWork.RadioData.GetById(radioData.Id);
            Mapper.Map(radioData, radio);
            TheUnitOfWork.RadioData.Update(radio);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.RadioData.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
    }
}

