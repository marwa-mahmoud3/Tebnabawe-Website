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
    public class AudiosLibraryService : AppServiceBase
    {
        public AudiosLibraryService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<AudioDto> GetAll()
        {
            IEnumerable<AudiosLibrary> allAudios = TheUnitOfWork.AudiosLibrary.GetAllAudios();
            return Mapper.Map<IEnumerable<AudioDto>>(allAudios);
        }
        public List<string> GetAllSheikhNames()
        {
            List<string> result = new List<string>();
            IEnumerable<AudiosLibrary> allAudios = TheUnitOfWork.AudiosLibrary.GetAllAudios();
            foreach(var item in allAudios)
            {
                if (!result.Contains(item.ShiekhName))
                {
                    result.Add(item.ShiekhName);
                }
            }
            return result;
        }
        public IEnumerable<AudioDto> GetAudioBySheikhName(string sheikhName)
        {
            IEnumerable<AudiosLibrary> allAudios = TheUnitOfWork.AudiosLibrary.GetWhere(e => e.ShiekhName == sheikhName);
            return Mapper.Map<IEnumerable<AudioDto>>(allAudios);
        }
        public AudioDto GetAudioById(int id)
        {
            return Mapper.Map<AudioDto>(TheUnitOfWork.AudiosLibrary.GetAudioById(id));
        }
        public bool Save(AudioDto audioDto)
        {
            if (audioDto == null)
                throw new ArgumentNullException();
            bool result = false;
            var audio = Mapper.Map<AudiosLibrary>(audioDto);
            if (TheUnitOfWork.AudiosLibrary.Insert(audio))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(AudioDto audioDto)
        {
            var audio = TheUnitOfWork.AudiosLibrary.GetById(audioDto.Id);
            Mapper.Map(audioDto, audio);
            TheUnitOfWork.AudiosLibrary.Update(audio);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.AudiosLibrary.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public IEnumerable<AudioDto> GetAudiosByPagination(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            var audios = TheUnitOfWork.AudiosLibrary.GetWhere(p => p.Id > 0)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return Mapper.Map<List<AudioDto>>(audios);
        }
        public int AudiosCount()
        {
            return TheUnitOfWork.AudiosLibrary.CountEntity();
        }
    }
}
