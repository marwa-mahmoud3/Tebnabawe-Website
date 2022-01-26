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
    public class VideosChannelController : ControllerBase
    {
        VideosChannelService _videosChannelService;
        public VideosChannelController(VideosChannelService videosChannelService)
        {
            this._videosChannelService = videosChannelService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_videosChannelService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_videosChannelService.GetVideoById(id));
        }
        [HttpGet("VideosByPagination/{pageSize},{pageNumber}")]
        public IActionResult GetVideosByPagination(int pageSize, int pageNumber)
        {
            return Ok(_videosChannelService.GetVideosByPagination(pageSize, pageNumber));
        }

        [HttpGet("VideosCount")]
        public IActionResult GetVideosCount()
        {
            return Ok(_videosChannelService.VideosCount());
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
                _videosChannelService.Save(videoDto);

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
                _videosChannelService.Update(videoDto);
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
                _videosChannelService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
    }
}
