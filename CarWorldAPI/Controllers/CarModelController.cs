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
    [Route("api/carModel")]
    [ApiController]
    public class CarModelController : ControllerBase
    {
        private readonly CarModelService _carService;
        public CarModelController(CarModelService carService)
        {
            _carService = carService;
        }

        //Create new car
        [HttpPost("CreateCarModel")]
        public async Task<IActionResult> CreateCarModel(CarItem carItem)
        {
            bool check = await _carService.CreateCarModel(carItem);
            if (check)
            {
                return Ok("Create new car successfully!");
            }
            else
            {
                return BadRequest("Create new car fail!");
            }
            
        }

        //Get all cars
        [HttpGet("GetAllCarModels")]
        public async Task<IActionResult> GetAllCarModels()
        {
            var result = await _carService.GetAllCarModels();
            return Ok(result);
        }

        [HttpGet("GetAllCarModelsByBrand")]
        public async Task<IActionResult> GetAllCarModelsByBrand(string brandId)
        {
            var result = await _carService.GetAllCarModelsByBrand(brandId);
            return Ok(result);
        }

        //Update car
        [HttpPut("UpdateCarModel")]
        public async Task<IActionResult> UpdateCarModel(string id, CarItem carItem)
        {
            bool check = await _carService.UpdateCarModel(id, carItem);
            if (check)
            {
                return Ok("Update car successfully!");
            }
            else
            {
                return BadRequest("Update car fail!");
            }
        }

        //Remove car
        [HttpDelete("RemoveCarModel")]
        public async Task<IActionResult> RemoveCarModel(string id)
        {
            bool check = await _carService.RemoveCarModel(id);
            if (check)
            {
                return Ok("Remove car successfully!");
            }
            else
            {
                return BadRequest("Remove car fail!");
            }
        }
    }
}
