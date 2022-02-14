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
    [Route("api/contestPrize")]
    [ApiController]
    public class ContestPrizeController : ControllerBase
    {
        private readonly ContestPrizeService _contestPrizeService;
        public ContestPrizeController(ContestPrizeService contestPrizeService)
        {
            _contestPrizeService = contestPrizeService;
        }

        [HttpPost("CreatePrizeForContest")]
        public async Task<IActionResult> CreatePrizeForContest([FromBody] ContestPrizeItem item)
        {
            bool check = await _contestPrizeService.CreatePrizeForContest(item);
            if (check)
            {
                return Ok("Create prize for contest successfully!");
            }
            else
            {
                return BadRequest("Create prize for contest fail!");
            }
        }

        [HttpGet("GetPrizesByContestId")]
        public async Task<IActionResult> GetPrizesByContestId(string contestId)
        {
            var result = await _contestPrizeService.GetPrizesByContestId(contestId);
            return Ok(result);
        }

        [HttpGet("GetJoinedUsers")]
        public async Task<IActionResult> GetJoinedUsers(string contestEventId)
        {
            var result = await _contestPrizeService.GetJoinedUsers(contestEventId);
            return Ok(result);
        }

        [HttpPut("UpdatePrizeForContest")]
        public async Task<IActionResult> UpdatePrizeForContest(string id, [FromBody] ContestPrizeItem item)
        {
            bool check = await _contestPrizeService.UpdatePrizeForContest(id, item);
            if (check)
            {
                return Ok("Update prize for contest successfully!");
            }
            else
            {
                return BadRequest("Update prize for contest fail!");
            }
        }

        

        [HttpDelete("RemovePrizeForContest")]
        public async Task<IActionResult> RemovePrizeForContest(string id)
        {
            bool check= await _contestPrizeService.RemovePrizeForContest(id);
            if (check)
            {
                return Ok("Remove prize for contest successfully!");
            }
            else
            {
                return BadRequest("Remove prize for contest fail!");
            }
        }


        
    }
}
