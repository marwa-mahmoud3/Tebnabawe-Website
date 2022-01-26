using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tebnabawe.Application.AudioT;
using Tebnabawe.Application.AudioT.Dto;
using Tebnabawe.Application.Authentication.Dto;

namespace Tebnabawe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AudiosLibraryController : ControllerBase
    {
        AudiosLibraryService _audiosLibraryService;
        public AudiosLibraryController(AudiosLibraryService audiosLibraryService)
        {
            this._audiosLibraryService = audiosLibraryService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_audiosLibraryService.GetAll());
        }
        [HttpGet("GetAllSheikhNames")]
        public IActionResult GetAllSheikhNames()
        {
            return Ok(_audiosLibraryService.GetAllSheikhNames());
        }
        [HttpGet("GetAudiosBySheikhName/{sheikhName}")]
        public IActionResult GetBySheikhName(string sheikhName)
        {
            return Ok(_audiosLibraryService.GetAudioBySheikhName(sheikhName));
        }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_audiosLibraryService.GetAudioById(id));
        }
        [HttpGet("AudiosByPagination/{pageSize},{pageNumber}")]
        public IActionResult GetAudiosByPagination(int pageSize, int pageNumber)
        {
            return Ok(_audiosLibraryService.GetAudiosByPagination(pageSize, pageNumber));
        }

        [HttpGet("AudiosCount")]
        public IActionResult GetAudiosCount()
        {
            return Ok(_audiosLibraryService.AudiosCount());
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPost]
        public IActionResult Create(AudioDto audioDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _audiosLibraryService.Save(audioDto);

                return Created("Created", audioDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, AudioDto audioDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _audiosLibraryService.Update(audioDto);
                return Ok(audioDto);
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
                _audiosLibraryService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
