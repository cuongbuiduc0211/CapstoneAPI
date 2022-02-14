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
    [Route("api/exchange")]
    [ApiController]
    public class ExchangeController : ControllerBase
    {
        private readonly ExchangeService _exchangeService;
        public ExchangeController(ExchangeService exchangeService)
        {
            _exchangeService = exchangeService;
        }

        [HttpPost("CreateCarExchange")]
        public async Task<IActionResult> CreateCarExchange([FromBody] ExchangeCarItem exchangeCarItem)
        {
            bool check = await _exchangeService.CreateCarExchange(exchangeCarItem);
            if (check)
            {
                return Ok("Create exchange successfully!");
            }
            else
            {
                return BadRequest("Create exchange fail!");
            }
        }

        [HttpPost("CreateAccessoryExchange")]
        public async Task<IActionResult> CreateAccessoryExchange(ExchangeAccItem exchangeAccItem)
        {
            bool check = await _exchangeService.CreateAccessoryExchange(exchangeAccItem);
            if (check)
            {
                return Ok("Create exchange successfully!");
            }
            else
            {
                return BadRequest("Create exchange fail!");
            }
        }

        [HttpGet("GetTopExCarBrandsByMonth")]
        public async Task<IActionResult> GetTopExCarBrandsByMonth(DateTime date)
        {
            var result = await _exchangeService.GetTopExCarBrandsByMonth(date);
            return Ok(result);
        }
        
        [HttpGet("CountExchangesByMonth")]
        public async Task<IActionResult> CountExchangesByMonth(ExchangeType type, DateTime date)
        {
            var result = await _exchangeService.CountExchangesByMonth(type, date);
            return Ok(result);
        }

        [HttpGet("GetInProcessExchanges")]
        public async Task<IActionResult> GetInProcessExchanges(ExchangeType type, int userId)
        {
            var result = await _exchangeService.GetInProcessExchanges(type, userId);
            return Ok(result);
        }
        [HttpGet("GetFinishedExchanges")]
        public async Task<IActionResult> GetFinishedExchanges(ExchangeType type, int userId)
        {
            var result = await _exchangeService.GetFinishedExchanges(type, userId);
            return Ok(result);
        }

        [HttpGet("GetAllExchanges")]
        public async Task<IActionResult> GetAllExchanges(ExchangeType type, int userId)
        {
            var result = await _exchangeService.GetAllExchanges(type, userId);
            return Ok(result);
        }

        [HttpGet("GetExchangesInCity")]
        public async Task<IActionResult> GetExchangesInCity(ExchangeType type, int userId, string cityId)
        {
            var result = await _exchangeService.GetExchangesInCity(type, userId, cityId);
            return Ok(result);
        }

        [HttpGet("GetExchangesInDistrict")]
        public async Task<IActionResult> GetExchangesInDistrict(ExchangeType type, int userId, string cityId, string districtId)
        {
            var result = await _exchangeService.GetExchangesInDistrict(type, userId, cityId, districtId);
            return Ok(result);
        }

        [HttpGet("GetExchangeById")]
        public async Task<IActionResult> GetExchangeById(string id)
        {
            var result = await _exchangeService.GetExchangeById(id);
            return Ok(result);
        }

        [HttpPut("CancelExchange")]
        public async Task<IActionResult> CancelExchange(string id)
        {
            bool check = await _exchangeService.CancelExchange(id);
            if (check)
            {
                return Ok("You have canceled the exchange!");
            }
            else
            {
                return BadRequest("Cancel the exchange fail!");
            }
        }
    }
}
