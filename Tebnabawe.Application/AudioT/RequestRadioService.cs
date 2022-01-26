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
using VisioForge.Libs.NAudio.Wave;

namespace Tebnabawe.Application.AudioT
{
    public class RequestRadioService : AppServiceBase
    {
        public RequestRadioService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public TimeSpan GetWavFileDuration(string fileName)
        {
            WaveFileReader WF = new WaveFileReader(fileName);
            return WF.TotalTime;
        }
        public IEnumerable<RequestRadioPageDto> GetAll()
        {
            IEnumerable<RequestRadioPage> all = TheUnitOfWork.RequestRadio.GetAllRequests();
            return Mapper.Map<IEnumerable<RequestRadioPageDto>>(all);
        }
        public RequestRadioPageDto GetRequestById(int id)
        {
            return Mapper.Map<RequestRadioPageDto>(TheUnitOfWork.RequestRadio.GetById(id));
        }
        // Save Request Radio Page for the first time 
        public bool Save(AudioDto audioDto)
        {
            bool result = false;
            if (RequestRadioCount() == 0)
            {
                RequestRadioPageDto requestRadio = new RequestRadioPageDto();
                requestRadio.TotalRadioTime = int.Parse((GetWavFileDuration(audioDto.AudioPath).TotalSeconds).ToString());
                requestRadio.StartTime = DateTime.Now;
                if (requestRadio == null)
                    throw new ArgumentNullException();
                var request = Mapper.Map<RequestRadioPage>(requestRadio);
                if (TheUnitOfWork.RequestRadio.Insert(request))
                {
                    result = TheUnitOfWork.Commit() > new int();
                }
                return result;
            }
            var requset = TheUnitOfWork.RequestRadio.GetAll().First();
            requset.TotalRadioTime+= int.Parse((GetWavFileDuration(audioDto.AudioPath).TotalSeconds).ToString());
            TheUnitOfWork.RequestRadio.Update(requset);
            return result;
        }
        public bool Update(AudioDto audioDto)
        {
            var request = TheUnitOfWork.RequestRadio.GetAllRequests().First();
            var oldDataAudio = TheUnitOfWork.RadioData.GetAll().FirstOrDefault(r => r.AudioId == audioDto.Id);
            request.TotalRadioTime -= oldDataAudio.AudioLength;
            request.TotalRadioTime += int.Parse((GetWavFileDuration(audioDto.AudioPath).TotalSeconds).ToString());
            TheUnitOfWork.RequestRadio.Update(request);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int AudioId)
        {
            var audioData = TheUnitOfWork.RadioData.GetAllRadiosData().First(a => a.AudioId == AudioId);
            var request = TheUnitOfWork.RequestRadio.GetAllRequests().First();
            request.TotalRadioTime -= audioData.AudioLength;
            TheUnitOfWork.RequestRadio.Update(request);
            return true;
        }
        public int RequestRadioCount()
        {
            return TheUnitOfWork.RequestRadio.CountEntity();
        }
    }
}