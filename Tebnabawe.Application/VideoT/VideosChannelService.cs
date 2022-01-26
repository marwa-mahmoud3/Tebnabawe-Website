using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Application.VideoT.Dto;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.VideoT
{
    public class VideosChannelService : AppServiceBase
    {
        public VideosChannelService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }

        public IEnumerable<VideoDto> GetAll()
        {
            IEnumerable<VideosChannel> allVideos= TheUnitOfWork.VideosChannel.GetAllVideos();
            return Mapper.Map<IEnumerable<VideoDto>>(allVideos);
        }
        public VideoDto GetVideoById(int id)
        {
            return Mapper.Map<VideoDto>(TheUnitOfWork.VideosChannel.GetVideoById(id));
        }
        public bool Save(VideoDto videoDto)
        {
            if (videoDto == null)
                throw new ArgumentNullException();
            bool result = false;
            var video = Mapper.Map<VideosChannel>(videoDto);
            if (TheUnitOfWork.VideosChannel.Insert(video))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(VideoDto videoDto)
        {
            var video = TheUnitOfWork.VideosChannel.GetById(videoDto.Id);
            Mapper.Map(videoDto, video);
            TheUnitOfWork.VideosChannel.Update(video);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.VideosChannel.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public IEnumerable<VideoDto> GetVideosByPagination(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            var videos = TheUnitOfWork.VideosChannel.GetWhere(p => p.Id > 0)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();

            return Mapper.Map<List<VideoDto>>(videos);
        }
        public int VideosCount()
        {
            return TheUnitOfWork.VideosChannel.CountEntity();
        }
    }
}
