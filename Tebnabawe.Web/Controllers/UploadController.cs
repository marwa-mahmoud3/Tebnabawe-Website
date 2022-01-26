using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Tebnabawe.Application.Authentication.Dto;

namespace Tebnabawe_API.Controllers
{
    [Authorize(Roles = UserRoleModel.Admin + "," + UserRoleModel.Supervisor)]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {

        [HttpPost, Route("{folderName}")]

        public async Task<ActionResult> Upload(string folderName)
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var folder_Name = Path.Combine("Resources", folderName);
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folder_Name);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    string fileExtention = fileName.Substring(fileName.LastIndexOf("."));
                    string fileNameWithoutExtension = fileName.Substring(0, fileName.IndexOf("."));
                    string newFileName = fileNameWithoutExtension + (folderName == "Audios" ? ".wav" : fileExtention);

                    var fullPath = Path.Combine(pathToSave, newFileName);

                    using (var stream = System.IO.File.Create(fullPath))
                    {
                        await file.CopyToAsync(stream);
                    }

                    return Ok(new { fileName = newFileName });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}