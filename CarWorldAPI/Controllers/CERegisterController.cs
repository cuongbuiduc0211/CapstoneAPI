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
    [Route("api/ceRegister")]
    [ApiController]
    public class CERegisterController : ControllerBase
    {
        private readonly CERegisterService _cERegisterService;
        public CERegisterController(CERegisterService cERegisterService)
        {
            _cERegisterService = cERegisterService;
        }
        [HttpPost("RegisterCE")]
        public async Task<IActionResult> RegisterCE(CERegister register)
        {
            CERegisStatus check = await _cERegisterService.RegisterCE(register);

            if (check != null)
            {
                return Ok(check);
            }
            else
            {
                return BadRequest("Register CE fail!");
            }
        }

        [HttpGet("GetCEsRegistered")]
        public async Task<IActionResult> GetCEsRegistered(ContestEventType type, int userId)
        {
            var result = await _cERegisterService.GetCEsRegistered(type, userId);
            return Ok(result);
        }

        [HttpGet("GetCEsJoined")]
        public async Task<IActionResult> GetCEsJoined(ContestEventType type, int userId)
        {
            var result = await _cERegisterService.GetCEsJoined(type, userId);
            return Ok(result);
        }

        [HttpGet("GetUsersInCE")]
        public async Task<IActionResult> GetUsersInCE(string contestEventId)
        {
            var result = await _cERegisterService.GetUsersInCE(contestEventId);
            return Ok(result);
        }

        [HttpPut("CancelRegisterCE")]
        public async Task<IActionResult> CancelRegisterCE(CERegister register)
        {
            bool check = await _cERegisterService.CancelRegisterCE(register);
            if (check)
            {
                return Ok("Cancel Register CE successfully!");
            }
            else
            {
                return BadRequest("Cancel Register CE fail!");
            }
        }

        [HttpPut("EvaluateCE")]
        public async Task<IActionResult> EvaluateCE(CERegister register, double evaluation)
        {
            bool check = await _cERegisterService.EvaluateCE(register, evaluation);
            if (check)
            {
                return Ok("Evaluate CE successfully!");
            }
            else
            {
                return BadRequest("Evaluate CE fail!");
            }
        }

        [HttpPut("CheckInUser")]
        public async Task<IActionResult> CheckInUser(CERegister register, UserEventContestStatus status)
        {
            bool check = await _cERegisterService.CheckInUser(register, status);
            if (check)
            {
                return Ok("Check In User successfully!");
            }
            else
            {
                return BadRequest("Check In User fail!");
            }
        }

        [HttpPut("CheckInUsers")]
        public async Task<IActionResult> CheckInUsers(CheckInItem items)
        {
            bool check = await _cERegisterService.CheckInUsers(items);
            if (check)
            {
                return Ok("Check In successfully!");
            }
            else
            {
                return BadRequest("Check In fail!");
            }
        }
    }
}
