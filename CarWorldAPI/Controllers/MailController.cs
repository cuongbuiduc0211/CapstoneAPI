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
    [Route("api/mail")]
    [ApiController]
    public class MailController : ControllerBase
    {
        private readonly MailService _mailService;
        public MailController(MailService mailService)
        {
            _mailService = mailService;
        }
        [HttpPost("SendMail")]
        public async Task<IActionResult> SendMail(MailRequest request)
        {
            await _mailService.SendEmailAsync(request);
            return Ok();
        }

        [HttpPost("SendMultiMail")]
        public async Task<IActionResult> SendMultiMail(MailRequests request)
        {
            await _mailService.SendMultiEmailAsync(request);
            return Ok();
        }
    }
}
