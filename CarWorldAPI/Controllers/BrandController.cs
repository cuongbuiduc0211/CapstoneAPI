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
    [Route("api/brand")] 
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly BrandService _brandService;
        public BrandController(BrandService brandService)
        {
            _brandService = brandService;
        }

        //Create new brand
        [HttpPost("CreateNewBrand")]
        public async Task<IActionResult> CreateNewBrand(BrandItem brandItem)
        {
            bool check = await _brandService.CreateNewBrand(brandItem);
            if (check)
            {
                return Ok("Create new brand successfully!");
            }
            else
            {
                return BadRequest("This brand is already existed!");
            }
        }

        //Get all brands
        [HttpGet("GetAllBrandsOfCar")]
        public async Task<IActionResult> GetAllBrandsOfCar()
        {
            var result = await _brandService.GetAllBrandsOfCar();
            return Ok(result);
        }

        //Get all brands
        [HttpGet("GetAllBrandsOfAccessory")]
        public async Task<IActionResult> GetAllBrandsOfAccessory()
        {
            var result = await _brandService.GetAllBrandsOfAccessory();
            return Ok(result);
        }

        //Get brand by id
        [HttpGet("GetBrandById")]
        public async Task<IActionResult> GetBrandById(string id)
        {
            var result = await _brandService.GetBrandById(id);
            return Ok(result);
        }

        //Get brand by name
        [HttpGet("GetBrandByName")]
        public async Task<IActionResult> GetBrandByName(string brandName)
        {
            var result = await _brandService.GetBrandByName(brandName);
            return Ok(result);
        }

        //Update brand
        [HttpPut("UpdateBrand")]
        public async Task<IActionResult> UpdateBrand(string id, BrandItem brandItem)
        {
            bool check = await _brandService.UpdateBrand(id, brandItem);
            if (check)
            {
                return Ok("Update brand successfully!");
            }
            else
            {
                return BadRequest("Update brand fail!");
            }
        }

        //Remove brand
        [HttpDelete("RemoveBrand")]
        public async Task<IActionResult> RemoveBrand(string id)
        {
            bool check = await _brandService.RemoveBrand(id);
            if (check)
            {
                return Ok("Remove brand successfully!");
            }
            else
            {
                return BadRequest("Remove brand fail!");
            }
        }
    }
}
