using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models;

namespace CarWorldAPI.Controllers
{
    [Route("api/attribution")]
    [ApiController]
    public class AttributionController : ControllerBase
    {
        private readonly AttributionService _attributionService;
        public AttributionController(AttributionService attributionService)
        {
            _attributionService = attributionService;
        }

        [HttpPost("CreateNewAttribution")]
        public async Task<IActionResult> CreateNewAttribution(List<AttributionItem> items)
        {
            List<ResponseAttribution> result = await _attributionService.CreateNewAttribution(items);
            return Ok(result);
        }

        [HttpGet("GetAttsByEngineType")]
        public async Task<IActionResult> GetAttsByEngineType(string typeId)
        {
            var result = await _attributionService.GetAllAttsbByType(typeId);
            return Ok(result);
        }

        [HttpPut("UpdateAttribution")]
        public async Task<IActionResult> UpdateAttribution(string id, AttributionItem item)
        {
            bool check = await _attributionService.UpdateAttribution(id, item);
            if (check)
            {
                return Ok("Update attribution successfully!");
            }
            else
            {
                return BadRequest("Update attribution fail!");
            }
        }

        [HttpDelete("RemoveAttribution")]
        public async Task<IActionResult> RemoveAttribution(string id)
        {
            bool check = await _attributionService.RemoveAttribution(id);
            if (check)
            {
                return Ok("Remove attribution successfully!");
            }
            else
            {
                return BadRequest("Remove attribution fail!");
            }

        }
    }
}
