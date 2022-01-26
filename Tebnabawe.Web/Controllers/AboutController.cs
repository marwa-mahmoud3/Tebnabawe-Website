using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Tebnabawe.Application.AboutT;
using Tebnabawe.Application.AboutT.Dto;
using Tebnabawe.Application.Authentication.Dto;

namespace Tebnabawe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AboutController : ControllerBase
    {
        AboutAppService _aboutAppService;
        public AboutController(AboutAppService aboutAppService)
        {
            this._aboutAppService = aboutAppService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_aboutAppService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_aboutAppService.GetAboutById(id));
        }
        [HttpGet("Admin/{id}")]
        public IActionResult GetByAdminId(string id)
        {
            return Ok(_aboutAppService.GetAboutByAdminId(id));
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPost]
        public IActionResult Create(AboutModel aboutModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _aboutAppService.Save(aboutModel);

                return Created("Created", aboutModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }

        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, AboutModel aboutModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _aboutAppService.Update(aboutModel);
                return Ok(aboutModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
