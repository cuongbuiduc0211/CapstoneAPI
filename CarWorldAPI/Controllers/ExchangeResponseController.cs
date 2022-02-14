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
    [Route("api/exchangeResponse")]
    [ApiController]
    public class ExchangeResponseController : ControllerBase
    {
        private readonly ExchangeResponseService _exchangeResponseService;
        public ExchangeResponseController(ExchangeResponseService exchangeResponseService)
        {
            _exchangeResponseService = exchangeResponseService;
        }

        [HttpPost("SendExResponse")]
        public async Task<IActionResult> SendExResponse([FromBody] ExchangeResItem exchangeResItem)
        {
            bool check = await _exchangeResponseService.SendExResponse(exchangeResItem);
            if (check)
            {
                return Ok("Send response successfully!");
            }
            else
            {
                return BadRequest("Exchange has been finished or canceled!");
            }

        }
       
        [HttpGet("GetExResponses")]
        public async Task<IActionResult> GetExResponses(string exchangeId)
        {
            var result = await _exchangeResponseService.GetExResponses(exchangeId);
            return Ok(result);
        }

        [HttpGet("GetInProcessResponses")]
        public async Task<IActionResult> GetInProcessResponses(int userId)
        {
            var result = await _exchangeResponseService.GetInProcessResponses(userId);
            return Ok(result);
        }

        [HttpGet("GetAcceptedResponses")]
        public async Task<IActionResult> GetAcceptedResponses(int userId)
        {
            var result = await _exchangeResponseService.GetAcceptedResponses(userId);
            return Ok(result);
        }

        [HttpPut("AcceptExResponse")]
        public async Task<IActionResult> AcceptExResponse(string exchangeId, int userId)
        {
            var result = await _exchangeResponseService.AcceptExResponse(exchangeId, userId);
            return Ok(result);
        }       
    }
}
