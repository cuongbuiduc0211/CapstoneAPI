using DatabaseAccess.Entities;
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
    [Route("api/prize")]
    [ApiController]
    public class PrizeController : ControllerBase
    {
        private readonly PrizeService _prizeService;
        public PrizeController(PrizeService prizeService)
        {
            _prizeService = prizeService;
        }

        [HttpPost("CreateNewPrize")]
        public async Task<IActionResult> CreateNewPrize([FromBody] PrizeItem prizeItem)
        {
            bool check = await _prizeService.CreatNewPrize(prizeItem);
            if (check)
            {
                return Ok("Create new prize successfully!");
            }
            else
            {
                return BadRequest("Create new prize fail!");
            }
        }

        [HttpGet("GetAllPrizes")]
        public async Task<IActionResult> GetAllPrizes()
        {
            var result = await _prizeService.GetAllPrizes();
            return Ok(result);
        }

        [HttpGet("GetPrizeById")]
        public async Task<IActionResult> GetPrizeById(string id)
        {
            var result = await _prizeService.GetPrizeById(id);
            return Ok(result);
        }

        [HttpGet("GetPrizesByName")]
        public async Task<IActionResult> GetPrizesByName(string name)
        {
            var result = await _prizeService.GetPrizeByName(name);
            return Ok(result);
        }

        [HttpPut("UpdatePrize")]
        public async Task<IActionResult> UpdatePrize(string id, [FromBody] PrizeItem prizeItem)
        {
            bool check = await _prizeService.UpdatePrize(id, prizeItem);
            if (check)
            {
                return Ok("Update prize successfully!");
            }
            else
            {
                return BadRequest("Update prize fail!");
            }
        }

        [HttpDelete("RemovePrize")]
        public async Task<IActionResult> RemovePrize(string id)
        {
            bool check = await _prizeService.RemovePrize(id);
            if (check)
            {
                return Ok("Remove prize successfully!");
            }
            else
            {
                return BadRequest("Remove prize fail!");
            }
        }

    }
}
