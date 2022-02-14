using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorldAPI.Controllers
{
    [Route("api/engineType")]
    [ApiController]
    public class EngineTypeController : ControllerBase
    {

        private readonly EngineTypeService _engineTypeService;
        public EngineTypeController(EngineTypeService engineTypeService)
        {
            _engineTypeService = engineTypeService;
        }

        [HttpPost("CreateEngineType")]
        public async Task<IActionResult> CreateEngineType(string name)
        {
            bool check = await _engineTypeService.CreateEngineType(name);
            if (check)
            {
                return Ok("Create new EngineType successfully!");
            }
            else
            {
                return BadRequest("Create new EngineType fail!");
            }
        }

        [HttpGet("GetEngineTypes")]
        public async Task<IActionResult> GetEngineTypes()
        {
            var result = await _engineTypeService.GetEngineTypes();
            return Ok(result);
        }

        [HttpPut("UpdateEngineType")]
        public async Task<IActionResult> UpdateEngineType(string id, string name)
        {
            bool check = await _engineTypeService.UpdateEngineType(id, name);
            if (check)
            {
                return Ok("Update EngineType successfully!");
            }
            else
            {
                return BadRequest("Update EngineType fail!");
            }
        }

        [HttpDelete("RemoveEngineType")]
        public async Task<IActionResult> RemoveEngineType(string id)
        {
            bool check = await _engineTypeService.RemoveEngineType(id);
            if (check)
            {
                return Ok("Remove EngineType successfully!");
            }
            else
            {
                return BadRequest("Remove EngineType fail!");
            }
        }
    }
}
