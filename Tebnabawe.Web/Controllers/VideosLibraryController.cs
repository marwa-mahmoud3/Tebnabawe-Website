using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Application.VideoT;
using Tebnabawe.Application.VideoT.Dto;

namespace Tebnabawe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideosLibraryController : ControllerBase
    {
        VideosLibraryService _videosLibraryService;
        public VideosLibraryController(VideosLibraryService videosLibraryService)
        {
            this._videosLibraryService = videosLibraryService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_videosLibraryService.GetAll());
        }
      
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_videosLibraryService.GetVideoById(id));
        }
        [HttpGet("VideosByPagination/{pageSize},{pageNumber}")]
        public IActionResult GetVideosByPagination(int pageSize, int pageNumber)
        {
            return Ok(_videosLibraryService.GetVideosByPagination(pageSize, pageNumber));
        }

        [HttpGet("VideosCount")]
        public IActionResult GetVideosCount()
        {
            return Ok(_videosLibraryService.VideosCount());
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPost]
        public IActionResult Create(VideoDto videoDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _videosLibraryService.Save(videoDto);

                return Created("Created", videoDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, VideoDto videoDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _videosLibraryService.Update(videoDto);
                return Ok(videoDto);
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
                _videosLibraryService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
