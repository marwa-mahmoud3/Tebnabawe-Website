using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Application.GoalsT;
using Tebnabawe.Application.GoalsT.Dto;

namespace Tebnabawe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoalsController : ControllerBase
    {
        GoalsAppService _goalsAppService;
        public GoalsController(GoalsAppService goalsAppService)
        {
            this._goalsAppService = goalsAppService;
        }
        [HttpGet("AllGoals/{AboutId}")]
        public IActionResult GetAll(int AboutId)
        {
            return Ok(_goalsAppService.GetAll(AboutId));
        }
        [HttpGet("GoalsByPagination/{AboutId}/{pageSize},{pageNumber}")]
        public IActionResult GetGoalsByPagination(int AboutId,int pageSize,int pageNumber)
        {
            return Ok(_goalsAppService.GetGoalsByPagination(pageSize,pageNumber));
        }

        [HttpGet("GoalsCount")]
        public IActionResult GetGoalsCount()
        {
            return Ok(_goalsAppService.GoalsCount());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_goalsAppService.GetById(id));
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPost]
        public IActionResult Create(GoalsModel goalsModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _goalsAppService.Save(goalsModel);

                return Created("Created", goalsModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, GoalsModel goalsModel)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _goalsAppService.Update(goalsModel);
                return Ok(goalsModel);
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
                _goalsAppService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}