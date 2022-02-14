using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Models;

namespace CarWorldAPI.Controllers
{
    [Route("api/genAtt")]
    [ApiController]
    public class GenerationAttributionController : ControllerBase
    {
        private readonly GenerationAttributionService _generationAttributionService;
        public GenerationAttributionController(GenerationAttributionService carAttributionService)
        {
            _generationAttributionService = carAttributionService;
        }
        [HttpPost("CreateCarWithAtts")]
        public async Task<IActionResult> CreateCarWithAtts(GenWithAttributions item)
        {
            bool check = await _generationAttributionService.CreateGenWithAtts(item);
            if (check)
            {
                return Ok("Create successfully!");
            }
            else
            {
                return BadRequest("Create fail!");
            }
        }
        [HttpGet("GetGenerationWithAtts")]
        public async Task<IActionResult> GetGenerationWithAtts(string generationId)
        {
            var result = await _generationAttributionService.GetGenerationWithAtts(generationId);
            return Ok(result);
        }
        [HttpPut("UpdateGenWithAtts")]
        public async Task<IActionResult> UpdateGenWithAtts(GenWithAttributions item)
        {
            bool check = await _generationAttributionService.UpdateGenWithAtts(item);
            if (check)
            {
                return Ok("Update successfully!");
            }
            else
            {
                return BadRequest("Update fail!");
            }
        }
        [HttpDelete("RemoveGenWithAtts")]
        public async Task<IActionResult> RemoveGenWithAtts(string generationId)
        {
            bool check = await _generationAttributionService.RemoveGenWithAtts(generationId);
            if (check)
            {
                return Ok("Remove successfully!");
            }
            else
            {
                return BadRequest("Remove fail!");
            }
        }
    }
}
