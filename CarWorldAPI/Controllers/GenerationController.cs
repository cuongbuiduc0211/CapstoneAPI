using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.Models;

namespace CarWorldAPI.Controllers
{
    [Route("api/generation")]
    [ApiController]
    public class GenerationController : ControllerBase
    {
        private readonly GenerationService _generationService;
        public GenerationController(GenerationService generationService)
        {
            _generationService = generationService;
        }
        [HttpPost("CreateGeneration")]
        public async Task<IActionResult> CreateGeneration(GenerationItem item)
        {
            bool check = await _generationService.CreateGeneration(item);
            if (check)
            {
                return Ok("Create new generation successfully!");
            }
            else
            {
                return BadRequest("Create new generation fail!");
            }
        }
        [HttpGet("GetAllGenerations")]
        public async Task<IActionResult> GetAllGenerations()
        {
            var result = await _generationService.GetAllGenerations();
            return Ok(result);
        }
        [HttpGet("GetAllGenerationsByBrand")]
        public async Task<IActionResult> GetAllGenerationsByBrand(string brandId)
        {
            var result = await _generationService.GetAllGenerationsByBrand(brandId);
            return Ok(result);
        }
        [HttpGet("GetAllGenerationsByCarModel")]
        public async Task<IActionResult> GetAllGenerationsByCarModel(string carModelId)
        {
            var result = await _generationService.GetAllGenerationsByCarModel(carModelId);
            return Ok(result);

        }
        [HttpPut("UpdateGeneration")]
        public async Task<IActionResult> UpdateGeneration(string id, GenerationItem item)
        {
            bool check = await _generationService.UpdateGeneration(id, item);
            if (check)
            {
                return Ok("Update generation successfully!");
            }
            else
            {
                return BadRequest("Update generation fail!");
            }
        }

        [HttpDelete("RemoveGeneration")]
        public async Task<IActionResult> RemoveGeneration(string id)
        {
            bool check = await _generationService.RemoveGeneration(id);
            if (check)
            {
                return Ok("Create new generation successfully!");
            }
            else
            {
                return BadRequest("Create new generation fail!");
            }
        }

    }


}
