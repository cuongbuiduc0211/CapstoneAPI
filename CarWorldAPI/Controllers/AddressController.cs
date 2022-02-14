using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarWorldAPI.Controllers
{
    [Route("api/address")]
    [ApiController]
    public class AddressController : ControllerBase
    {

        private readonly AddressService _addressService;
        public AddressController(AddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet("GetCities")]
        public async Task<IActionResult> GetCities()
        {
            var result = await _addressService.GetCities();
            return Ok(result);
        }

        [HttpGet("GetDistricts")]
        public async Task<IActionResult> GetDistricts(string cityId)
        {
            var result = await _addressService.GetDistricts(cityId);
            return Ok(result);
        }

        [HttpGet("GetWards")]
        public async Task<IActionResult> GetWards(string districtId)
        {
            var result = await _addressService.GetWards(districtId);
            return Ok(result);
        }
    }
}
