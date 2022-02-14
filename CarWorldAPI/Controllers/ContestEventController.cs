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
    [Route("api/contestEvent")]
    [ApiController]
    public class ContestEventController : ControllerBase
    {
        private readonly ContestEventService _contestEventService;
        public ContestEventController(ContestEventService contestEventService)
        {
            _contestEventService = contestEventService;
        }

        [HttpPost("CreateCE")]
        public async Task<IActionResult> CreateCE([FromBody] ContestEventItem item)
        {
            bool check = await _contestEventService.CreateCE(item);
            if (check)
            {
                return Ok("Create new CE successfully!");
            }
            else
            {
                return BadRequest("Create new CE fail!");
            }
        }

        [HttpGet("CountCEsByMonth")]
        public async Task<IActionResult> CountCEsByMonth(ContestEventType type, DateTime date)
        {
            var result = await _contestEventService.CountCEsByMonth(type, date);
            return Ok(result);
        }

        [HttpGet("GetCanceledCEs")]
        public async Task<IActionResult> GetCanceledCEs(ContestEventType type)
        {
            var result = await _contestEventService.GetCanceledCEs(type);
            return Ok(result);
        }

        [HttpGet("GetAllContestPrizes")]
        public async Task<IActionResult> GetAllContestPrizes(DateTime now)
        {
            var result = await _contestEventService.GetAllContestPrizes(now);
            return Ok(result);
        }

        
        [HttpGet("GetCEsMobile")]
        public async Task<IActionResult> GetCEsMobile(ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetCEsMobile(type, now);
            return Ok(result);
        }

        [HttpGet("GetCEsByBrandMobile")]
        public async Task<IActionResult> GetCEsByBrandMobile(ContestEventType type, string brandId, DateTime now)
        {
            var result = await _contestEventService.GetCEsByBrandMobile(type, brandId, now);
            return Ok(result);
        }

        [HttpPost("GetCEsByUserInterestedBrands")]
        public async Task<IActionResult> GetCEsByUserInterestedBrands(ContestEventType type, List<string> interestedBrands, DateTime now)
        {
            var result = await _contestEventService.GetCEsByUserInterestedBrands(type, interestedBrands, now);
            return Ok(result);
        }

        [HttpGet("GetOngoingCEsMobile")]
        public async Task<IActionResult> GetOngoingCEsMobile(ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetOngoingCEsMobile(type, now);
            return Ok(result);
        }

        [HttpGet("GetRegisterCEsWeb")]
        public async Task<IActionResult> GetRegisterCEsWeb(ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetRegisterCEsWeb(type, now);
            return Ok(result);
        }

        [HttpGet("GetRegisterCEsByBrandWeb")]
        public async Task<IActionResult> GetRegisterCEsByBrandWeb(string brandId, ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetRegisterCEsByBrandWeb(brandId, type, now);
            return Ok(result);
        }

        [HttpGet("GetPreparedCEsWeb")]
        public async Task<IActionResult> GetPreparedCEsWeb(ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetPreparedCEsWeb(type, now);
            return Ok(result);
        }

        [HttpGet("GetPreparedCEsByBrandWeb")]
        public async Task<IActionResult> GetPreparedCEsByBrandWeb(string brandId, ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetPreparedCEsByBrandWeb(brandId, type, now);
            return Ok(result);
        }

        [HttpGet("GetOngoingCEsWeb")]
        public async Task<IActionResult> GetOngoingCEsWeb(ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetOngoingCEsWeb(type, now);
            return Ok(result);
        }

        [HttpGet("GetOngoingCEsByBrandWeb")]
        public async Task<IActionResult> GetOngoingCEsByBrandWeb(string brandId, ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetOngoingCEsByBrandWeb(brandId, type, now);
            return Ok(result);
        }

        [HttpGet("GetFinishedCEsWeb")]
        public async Task<IActionResult> GetFinishedCEsWeb(ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetFinishedCEsWeb(type, now);
            return Ok(result);
        }

        [HttpGet("GetFinishedCEsByBrandWeb")]
        public async Task<IActionResult> GetFinishedCEsByBrandWeb(string brandId, ContestEventType type, DateTime now)
        {
            var result = await _contestEventService.GetFinishedCEsByBrandWeb(brandId, type, now);
            return Ok(result);
        }

        [HttpGet("GetCEById")]
        public async Task<IActionResult> GetCEById(string id)
        {
            var result = await _contestEventService.GetCEById(id);
            return Ok(result);
        }
        [HttpGet("RatingCE")]
        public async Task<IActionResult> RatingCE(string id)
        {
            var result = await _contestEventService.RatingCE(id);
            return Ok(result);
        }
        [HttpPut("UpdateCE")]
        public async Task<IActionResult> UpdateCE(string id, ContestEventItem item)
        {
            bool check = await _contestEventService.UpdateCE(id, item);
            if (check)
            {
                return Ok("Update CE successfully!");
            }
            else
            {
                return BadRequest("Update CE fail!");
            }
        }

        [HttpPut("CancelCE")]
        public async Task<IActionResult> CancelCE(string id, string reason)
        {
            bool check = await _contestEventService.CancelCE(id, reason);
            if (check)
            {
                return Ok("Cancel CE successfully!");
            }
            else
            {
                return BadRequest("Cancel CE fail!");
            }
        }
    }
}
