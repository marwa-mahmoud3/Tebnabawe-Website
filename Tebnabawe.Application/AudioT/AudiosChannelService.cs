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
    public class AudiosChannelService : AppServiceBase
    {
        public AudiosChannelService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public TimeSpan GetWavFileDuration(string fileName)
        {
            WaveFileReader WF = new WaveFileReader(fileName);
            return WF.TotalTime;
        }
        public IEnumerable<AudioDto> GetAll()
        {
            IEnumerable<AudiosChannel> allAudios = TheUnitOfWork.AudiosChannel.GetAllAudios();
            return Mapper.Map<IEnumerable<AudioDto>>(allAudios);
        }
        public AudioDto GetAudioById(int id)
        {
            return Mapper.Map<AudioDto>(TheUnitOfWork.AudiosChannel.GetAudioById(id));
        }
        public bool Save(AudioDto audioDto)
        {
            if (audioDto == null)
                throw new ArgumentNullException();
            bool result = false;
            var audio = Mapper.Map<AudiosChannel>(audioDto);
            audio.Date = DateTime.Now.ToString("dd/MM/yyyy");
            if (TheUnitOfWork.AudiosChannel.Insert(audio))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            RadioData radioData = new RadioData();
            radioData.AudioId = audio.Id;
            radioData.AudioPath = audio.AudioPath;
            radioData.AudioLength= int.Parse((GetWavFileDuration(audio.AudioPath).TotalSeconds).ToString());
            //if(TheUnitOfWork.RadioData.Insert(radioData))
            //{
            //    result = TheUnitOfWork.Commit() > new int();
            //}
            return result;
        }
        public bool Update(AudioDto audioDto)
        {
            var audio = TheUnitOfWork.AudiosChannel.GetById(audioDto.Id);
            Mapper.Map(audioDto, audio);
            audioDto.Date = DateTime.Now.ToString("dd/MM/yyyy");
            TheUnitOfWork.AudiosChannel.Update(audio);
            TheUnitOfWork.Commit();
            RadioData radioData = TheUnitOfWork.RadioData.GetAll().FirstOrDefault(r => r.AudioId == audio.Id);
            radioData.AudioId = audio.Id;
            radioData.AudioPath = audioDto.AudioPath;
            radioData.AudioLength = int.Parse((GetWavFileDuration(audio.AudioPath).TotalSeconds).ToString());
            TheUnitOfWork.RadioData.Update(radioData);
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.AudiosChannel.Delete(id);

            result = TheUnitOfWork.Commit() > new int();
            int radioDataId = TheUnitOfWork.RadioData.GetAll().FirstOrDefault(r => r.AudioId == id).Id;
            TheUnitOfWork.RadioData.Delete(radioDataId);
            return result;
        }
        public IEnumerable<AudioDto> GetAudiosByPagination(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            var audios = TheUnitOfWork.AudiosChannel.GetWhere(p => p.Id > 0)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return Mapper.Map<List<AudioDto>>(audios);
        }
        public int AudiosCount()
        {
            return TheUnitOfWork.AudiosChannel.CountEntity();
        }
    }
}