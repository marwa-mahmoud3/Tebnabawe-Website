using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Application.BrochuresT;
using Tebnabawe.Application.BrochuresT.Dto;

namespace Tebnabawe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrochuresController : ControllerBase
    {
        BrochuresAppService _brochuresAppService;
        public BrochuresController(BrochuresAppService brochuresAppService)
        {
            this._brochuresAppService = brochuresAppService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_brochuresAppService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_brochuresAppService.GetBrochureById(id));
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPost]
        public IActionResult Create(BrochuresDto brochuresDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _brochuresAppService.Save(brochuresDto);

                return Created("Created", brochuresDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [HttpGet("BrochuresByPagination/{pageSize},{pageNumber}")]
        public IActionResult GetGoalsByPagination(int pageSize, int pageNumber)
        {
            return Ok(_brochuresAppService.GetBrochuresByPagination(pageSize, pageNumber));
        }

        [HttpGet("BrochuresCount")]
        public IActionResult GetBrochuresCount()
        {
            return Ok(_brochuresAppService.BrochuresCount());
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, BrochuresDto brochuresDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _brochuresAppService.Update(brochuresDto);
                return Ok(brochuresDto);
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
                _brochuresAppService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
