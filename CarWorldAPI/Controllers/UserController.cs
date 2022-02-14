using DatabaseAccess.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utility.Enum;
using Utility.Models;

namespace CarWorldAPI.Controllers
{
    [Route("api/user")]
    [ApiController]
    //[Authorize]
    public class UserController : ControllerBase
    {
        
        private readonly UserService _userService;
        public UserController(UserService userService)
        {
            
            _userService = userService;
        }
        

        //login for admins and managers (web)
        [AllowAnonymous]
        [HttpPost("LoginAdmin")]
        public async Task<IActionResult> LoginAdmin([FromBody] AdminAccount adminAccount)
        {
            User user = await _userService.CheckAdminLogin(adminAccount);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Username or password is incorrect");
            }
        }

        //login by gmail for users (mobile)
        [AllowAnonymous]
        [HttpPost("LoginUser")]
        public async Task<IActionResult> LoginUser([FromBody] UserAccount userAccount)
        {
            User user = await _userService.CheckUserLogin(userAccount);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest("Email is not valid");
            }
            
        }
        [AllowAnonymous]
        [HttpPost("RegisterAccount")]
        public async Task<IActionResult> RegisterAccount([FromBody] SelfProfile selfProfile)
        {
            bool check = await _userService.RegisterAccount(selfProfile);
            if (check)
            {
                return Ok("Register successfully!");
            }
            else
            {
                return BadRequest("Register fail!");
            }

        }

        [HttpPost("ChooseInterestedBrand")]
        public async Task<IActionResult> ChooseInterestedBrand(InterestedBrandItem item)
        {
            bool check = await _userService.ChooseInterestedBrand(item);
            if (check)
            {
                return Ok("Choose successfully!");
            }
            else
            {
                return BadRequest("Choose fail!");
            }
        }
        [HttpGet("GetUserInterestedBrands")]
        public async Task<IActionResult> GetUserInterestedBrands(int userId)
        {
            var result = await _userService.GetUserInterestedBrands(userId);
            return Ok(result);
        }
        //Create new account (role: admin / manager)
        //[Authorize(Roles = Utility.Enum.Role.Admin)]
        [HttpPost("CreateNewAccount")]
        public async Task<IActionResult> CreateNewAccount([FromBody] NewAccount adminAccount)
        {
            bool check = await _userService.CreateNewAccount(adminAccount);
            if (check)
            {
                return Ok("Create new account successfully!");
            }
            else
            {
                return BadRequest("Create new account fail!");
            }
            
        }

        //Get all users
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _userService.GetAllUsers();
            return Ok(result);
        }

        [HttpGet("GetAllAdminsAndManagers")]
        public async Task<IActionResult> GetAllAdminsAndManagers()
        {
            var result = await _userService.GetAllAdminsAndManagers();
            return Ok(result);
        }

        //Get user by id
        [HttpGet("GetUserById")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var result = await _userService.GetUserById(id);
            return Ok(result);
        }

        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var result = await _userService.GetUserByEmail(email);
            return Ok(result);
        }

        //Get user by id
        [HttpGet("GetUserByFullName")]
        public async Task<IActionResult> GetUserByFullName(string fullName)
        {
            var result = await _userService.GetUserByFullName(fullName);
            return Ok(result);
        }

        //Update profile
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile(int id, [FromBody] SelfProfile selfProfile)
        {
            bool check = await _userService.UpdateSelfProfile(id, selfProfile);
            if (check)
            {
                return Ok("Update profile successfully!");
            }
            else
            {
                return BadRequest("Update profile fail!");
            }
           
        }

        [HttpPut("ChangePassword")]
        public async Task<IActionResult> ChangePassword(int id, [FromBody] ChangePassword changePassword)
        {
            bool check = await _userService.ChangePassword(id, changePassword);
            if (check)
            {
                return Ok("Change password successfully!");
            }
            else
            {
                return Ok("Change password fail!");
            }
        }
       
        //Admin change account status
        [HttpPut("ChangeAccountStatus")]
        
        public async Task<IActionResult> ChangeAccountStatus(int id, UserStatus status)
        {
            bool check = await _userService.ChangeAccountStatus(id, status);
            if (check)
            {
                return Ok("Change status successfully!");
            }
            else
            {
                return BadRequest("Change status fail!");
            }
        }

    }
}
