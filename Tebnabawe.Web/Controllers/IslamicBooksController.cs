using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tebnabawe.Application.Authentication.Dto;
using Tebnabawe.Application.IslamicBooksT;
using Tebnabawe.Application.IslamicBooksT.Dto;

namespace Tebnabawe.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IslamicBooksController : ControllerBase
    {
        IslamicBooksAppService _islamicBooksAppService;
        public IslamicBooksController(IslamicBooksAppService islamicBooksAppService)
        {
            this._islamicBooksAppService = islamicBooksAppService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_islamicBooksAppService.GetAll());
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(_islamicBooksAppService.GetIslamicBooksById(id));
        }
        [HttpGet("BooksByPagination/{pageSize},{pageNumber}")]
        public IActionResult GetGoalsByPagination(int pageSize, int pageNumber)
        {
            return Ok(_islamicBooksAppService.GetBooksByPagination(pageSize, pageNumber));
        }

        [HttpGet("BooksCount")]
        public IActionResult GetBooksCount()
        {
            return Ok(_islamicBooksAppService.BooksCount());
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPost]
        public IActionResult Create(IslamicBooksDto islamicBooksDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                islamicBooksDto.Data = DateTime.Now.ToString("dd/mm/yyyy");
                _islamicBooksAppService.Save(islamicBooksDto);

                return Created("Created", islamicBooksDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
        [HttpPut("{id}")]
        public IActionResult Edit(int id, IslamicBooksDto islamicBooksDto)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            try
            {
                _islamicBooksAppService.Update(islamicBooksDto);
                return Ok(islamicBooksDto);
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
                _islamicBooksAppService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
