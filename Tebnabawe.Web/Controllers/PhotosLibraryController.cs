using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Application.PhotosLibraryT;
using Tebnabawe.Application.PhotosLibraryT.Dto;

namespace Tebnabawe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosLibraryController : ControllerBase
    {
        PhotosLibraryAppService _photosLibraryAppService;
        public PhotosLibraryController(PhotosLibraryAppService photosLibraryAppService)
        {
            this._photosLibraryAppService = photosLibraryAppService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_photosLibraryAppService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_photosLibraryAppService.GetPhotoById(id));
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPost]
        public IActionResult Create(PhotosLibraryModel photosLibraryModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                photosLibraryModel.Date = DateTime.Now;
                _photosLibraryAppService.Save(photosLibraryModel);

                return Created("Created", photosLibraryModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, PhotosLibraryModel photosLibraryModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _photosLibraryAppService.Update(photosLibraryModel);
                return Ok(photosLibraryModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _photosLibraryAppService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("PhotosLibraryByPagination/{pageSize},{pageNumber}")]
        public IActionResult GetPhotosLibraryByPagination(int pageSize, int pageNumber)
        {
            return Ok(_photosLibraryAppService.GetPhotosByPagination(pageSize, pageNumber));
        }

        [HttpGet("PhotosLibraryCount")]
        public IActionResult GetPhotosLibraryCount()
        {
            return Ok(_photosLibraryAppService.PhotosLibraryCount());
        }
    }
}