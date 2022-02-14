using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/accessory")]
    [ApiController]
    //[Authorize]
    public class AccessoryController : ControllerBase
    {

        private readonly AccessoryService _accessoryService;
        public AccessoryController(AccessoryService accessoryService)
        {
            _accessoryService = accessoryService;
        }

        
        [HttpPost("CreateNewAccessory")]
        public async Task<IActionResult> CreateNewAccessory([FromBody] AccessoryItem accessoryItem)
        {         
            bool check = await _accessoryService.CreateNewAccessory(accessoryItem);
            if (check)
            {
                return Ok("Create new accessory successfully!");
            }
            else
            {
                return BadRequest("This accessory is already existed!");
            }
        }

        //Get all accessories
        [HttpGet("GetAllAccessories")]
        public async Task<IActionResult> GetAllAccessories()
        {
            var result = await _accessoryService.GetAllAccessories();
            return Ok(result);
        }

        //Get accessory by id
        [HttpGet("GetAccessoryById")]
        public async Task<IActionResult> GetAccessoryById(string id)
        {
            var result = await _accessoryService.GetAccessoryById(id);
            return Ok(result);
        }

        //Get accessory by name
        [HttpGet("GetAccessoriesByName")]
        public async Task<IActionResult> GetAccessoriesByName(string accessoryName)
        {
            var result = await _accessoryService.GetAccessoriesByName(accessoryName);
            return Ok(result);
        }

        [HttpGet("GetAccessoryByBrand")]
        public async Task<IActionResult> GetAccessoriesByBrand(string brandName)
        {
            var result = await _accessoryService.GetAccessoriesByBrand(brandName);
            return Ok(result);
        }

        //Update accessory
        [HttpPut("UpdateAccessory")]
        public async Task<IActionResult> UpdateAccessory(string id, [FromBody] AccessoryItem accessoryItem)
        {
            bool check = await _accessoryService.UpdateAccessory(id, accessoryItem);
            if (check)
            {
                return Ok("Update accessory successfully!");
            }
            else
            {
                return BadRequest("Update accessory fail!");
            }
        }

        //Remove accessory
        [HttpDelete("RemoveAccessory")]
        public async Task<IActionResult> RemoveAccessory(string id)
        {
            bool check = await _accessoryService.RemoveAccessory(id);
            if (check)
            {
                return Ok("Remove accessory successfully!");
            }
            else
            {
                return BadRequest("Remove accessory fail!");
            }
            
        }
    }
}
