using AutoMapper;
using Tebnabawe.Application.AboutT.Dto;
using Tebnabawe.Application.AudioT.Dto;
using Tebnabawe.Application.Authentication;
using Tebnabawe.Application.BrochuresT.Dto;
using Tebnabawe.Application.GoalsT.Dto;
using Tebnabawe.Application.IslamicBooksT.Dto;
using Tebnabawe.Application.NewsT.Dto;
using Tebnabawe.Application.PhotosLibraryT.Dto;
using Tebnabawe.Application.VideoT.Dto;
using Tebnabawe.Application.WorksT.Dto;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.Configuration
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            this.CreateMap<About,AboutModel>().ReverseMap();
            this.CreateMap<Goals, GoalsModel>().ReverseMap();
            this.CreateMap<Works, WorkModel>().ReverseMap();
            this.CreateMap<News, NewsDto>().ReverseMap();
            this.CreateMap<Brochures, BrochuresDto>().ReverseMap();
            this.CreateMap<IslamicBooks, IslamicBooksDto>().ReverseMap();
            this.CreateMap<PhotosLibrary, PhotosLibraryModel>().ReverseMap();
            this.CreateMap<RegisterModel, ApplicationUser>().ReverseMap();
            this.CreateMap<AudiosChannel, AudioDto>().ReverseMap();
            this.CreateMap<AudiosLibrary, AudioDto>().ReverseMap();
            this.CreateMap<VideosChannel, VideoDto>().ReverseMap();
            this.CreateMap<VideosLibrary, VideoDto>().ReverseMap();
            this.CreateMap<RadioData,RadioDataDto>().ReverseMap();
            this.CreateMap<RequestRadioPage, RequestRadioPageDto>().ReverseMap();
        }
    }
}
