using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tebnabawe.Application.Bases;
using Tebnabawe.Application.PhotosLibraryT.Dto;
using Tebnabawe.Bases.Interfaces;
using Tebnabawe.Data.Models;

namespace Tebnabawe.Application.PhotosLibraryT
{
   public class PhotosLibraryAppService: AppServiceBase
    {
        public PhotosLibraryAppService(IUnitOfWork theUnitOfWork, IMapper mapper) : base(theUnitOfWork, mapper)
        {

        }
        public IEnumerable<PhotosLibraryModel> GetAll()
        {
            IEnumerable<PhotosLibrary> allPhotos = TheUnitOfWork.PhotosLibrary.GetAllPhotos();
            return Mapper.Map<IEnumerable<PhotosLibraryModel>>(allPhotos);
        }
        public PhotosLibraryModel GetPhotoById(int id)
        {
            return Mapper.Map<PhotosLibraryModel>(TheUnitOfWork.PhotosLibrary.GetPhotoById(id));
        }
        public bool Save(PhotosLibraryModel photosLibraryModel)
        {
            if (photosLibraryModel == null)
                throw new ArgumentNullException();
            bool result = false;
            var photo = Mapper.Map<PhotosLibrary>(photosLibraryModel);
            if (TheUnitOfWork.PhotosLibrary.Insert(photo))
            {
                result = TheUnitOfWork.Commit() > new int();
            }
            return result;
        }
        public bool Update(PhotosLibraryModel photosLibraryModel)
        {
            var photo = TheUnitOfWork.PhotosLibrary.GetById(photosLibraryModel.Id);
            Mapper.Map(photosLibraryModel, photo);
            TheUnitOfWork.PhotosLibrary.Update(photo);
            TheUnitOfWork.Commit();
            return true;
        }
        public bool Delete(int id)
        {
            bool result = false;

            TheUnitOfWork.PhotosLibrary.Delete(id);
            result = TheUnitOfWork.Commit() > new int();

            return result;
        }
        public IEnumerable<PhotosLibraryModel> GetPhotosByPagination(int pageSize, int pageNumber)
        {
            pageSize = (pageSize <= 0) ? 10 : pageSize;
            pageNumber = (pageNumber < 1) ? 0 : pageNumber - 1;
            var photos = TheUnitOfWork.PhotosLibrary.GetWhere(p => p.Id>0)
                .Skip(pageNumber * pageSize).Take(pageSize)
                .ToList();
            return Mapper.Map<List<PhotosLibraryModel>>(photos);
        }
        public int PhotosLibraryCount()
        {
            return TheUnitOfWork.PhotosLibrary.CountEntity();
        }
    }
}