using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Application.NewsT;
using Tebnabawe.Application.NewsT.Dto;

namespace Tebnabawe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        NewsAppService _newsAppService;
        public NewsController(NewsAppService newsAppService)
        {
            this._newsAppService = newsAppService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_newsAppService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_newsAppService.GetNewsById(id));
        }
        [HttpGet("NewsByPagination/{pageSize},{pageNumber}")]
        public IActionResult GetNewsByPagination(int pageSize, int pageNumber)
        {
            return Ok(_newsAppService.GetNewsByPagination(pageSize, pageNumber));
        }

        [HttpGet("NewsCount")]
        public IActionResult GetNewsCount()
        {
            return Ok(_newsAppService.NewsCount());
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPost]
        public IActionResult Create(NewsDto newsDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                newsDto.Date = DateTime.Now;
                _newsAppService.Save(newsDto);

                return Created("Created", newsDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, NewsDto newsDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _newsAppService.Update(newsDto);
                return Ok(newsDto);
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
                _newsAppService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
