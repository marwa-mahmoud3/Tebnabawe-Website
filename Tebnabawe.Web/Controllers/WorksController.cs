using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Application.WorksT.Dto;
using Tebnabawe.Application.WorkT;

namespace Tebnabawe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WorksController : ControllerBase
    {
        WorkAppService _workAppService;
        public WorksController(WorkAppService workAppService)
        {
            this._workAppService = workAppService;
        }
        [HttpGet("AllWorks/{AboutId}")]
        public IActionResult GetAll(int AboutId)
        {
            return Ok(_workAppService.GetAll(AboutId));
        }
        [HttpGet("WorksByPagination/{AboutId}/{pageSize},{pageNumber}")]
        public IActionResult GetGoalsByPagination(int AboutId, int pageSize, int pageNumber)
        {
            return Ok(_workAppService.GetWorksByPagination(pageSize, pageNumber));
        }

        [HttpGet("WorksCount")]
        public IActionResult GetWorksCount()
        {
            return Ok(_workAppService.WorksCount());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_workAppService.GetById(id));
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPost]
        public IActionResult Create(WorkModel workModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _workAppService.Save(workModel);

                return Created("Created", workModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, WorkModel workModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _workAppService.Update(workModel);
                return Ok(workModel);
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
                _workAppService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}