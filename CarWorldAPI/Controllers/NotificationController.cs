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
    [Route("api/notification")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly NotificationService _notificationService;
        public NotificationController(NotificationService notificationService)
        {
            _notificationService = notificationService;
        }
        [HttpPost("SendNoti")]
        public async Task<IActionResult> SendNoti(string clientToken, string title, string body)
        {
            var result = await _notificationService.SendNoti(clientToken, title, body);
            return Ok(result);
        }
        [HttpPost("SendNotiToMobile")]
        public async Task<IActionResult> SendNotiToMobile(string clientToken, string title, string body,string image, string redirect)
        {
            var result = await _notificationService.SendNotiToMobile(clientToken, title, body, image, redirect);
            return Ok(result);
        }
        //[HttpPost("SendNotification")]
        //public async Task<IActionResult> SendNotification(NotificationModel notiModel)
        //{
        //    var result = await _notificationService.SendNotification(notiModel);
        //    return Ok(result);
        //}
    }
}
