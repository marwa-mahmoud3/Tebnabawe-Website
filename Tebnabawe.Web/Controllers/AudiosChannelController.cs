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
    public class AudiosChannelController : ControllerBase
    {
        AudiosChannelService _audiosChannelService;
        RequestRadioService _requestRadioService;
        RadioDataService _radioDataService;
        public AudiosChannelController(AudiosChannelService audiosChannelService, RequestRadioService requestRadioService,RadioDataService radioDataService)
        {
            this._audiosChannelService = audiosChannelService;
            this._requestRadioService = requestRadioService;
            this._radioDataService = radioDataService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_audiosChannelService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_audiosChannelService.GetAudioById(id));
        }
        [HttpGet("AudiosByPagination/{pageSize},{pageNumber}")]
        public IActionResult GetAudiosByPagination(int pageSize, int pageNumber)
        {
            return Ok(_audiosChannelService.GetAudiosByPagination(pageSize, pageNumber));
        }

        [HttpGet("AudiosCount")]
        public IActionResult GetAudiosCount()
        {
            return Ok(_audiosChannelService.AudiosCount());
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
                _requestRadioService.Save(audioDto);
                _audiosChannelService.Save(audioDto);
                
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
                _requestRadioService.Update(audioDto);
                _audiosChannelService.Update(audioDto);
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
                _requestRadioService.Delete(id);
                _audiosChannelService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("NextAudio")]
        public IActionResult GetNextAudio()
        {

            return Ok(_audiosChannelService.AudiosCount());
        }
        [HttpGet("CurrentAudio")]
        public IActionResult GetCurrentAudio()
        {
            var request = _requestRadioService.GetAll().First();
            int tolalRadioDuration = request.TotalRadioTime;
            TimeSpan StartTime = request.StartTime.TimeOfDay;

            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            int start=int.Parse((StartTime.TotalSeconds).ToString());
            int current= int.Parse((currentTime.TotalSeconds).ToString());
            int Duration = (current - start) % tolalRadioDuration;

            long sum = 0;
            var allRadioData = _radioDataService.GetAll();
            int requiredAudioDataId = allRadioData.First().Id;
            if (Duration == 0) return Ok(new { requiredDataRadioId = requiredAudioDataId, rquiredStartSecond = Duration });
            foreach (var audio in allRadioData)
            {
                if (sum + audio.AudioLength >= Duration) 
                {
                    return Ok(new { requiredDataRadioId = requiredAudioDataId, rquiredStartSecond = (Duration - sum) });
                }
                sum += audio.AudioLength;
            }
            return Ok();
        }
    }
}