using DatabaseAccess.Entities;
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
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly FeedbackService _feedbackService;
        public FeedbackController(FeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpPost("FeedbackCE")]
        public async Task<IActionResult> FeedbackCE(string contestEventId, FeedbackItem feedbackItem)
        {
            bool check = await _feedbackService.FeedbackCE(contestEventId, feedbackItem);
            if (check)
            {
                return Ok("Thank you for your feedback!");
            }
            else
            {
                return BadRequest("Feedback fail!");
            }
        }

        [HttpPost("FeedbackExchange")]
        public async Task<IActionResult> FeedbackExchange(string exchangeId, [FromBody] FeedbackItem feedbackItem)
        {
            bool check = await _feedbackService.FeedbackExchange(exchangeId, feedbackItem);
            if (check)
            {
                return Ok("Thank you for your feedback!");
            }
            else
            {
                return BadRequest("Feedback fail!");
            }
        }      

        [HttpPost("FeedbackExResponse")]
        public async Task<IActionResult> FeedbackExResponse(string exResId, [FromBody] FeedbackItem feedbackItem)
        {
            bool check = await _feedbackService.FeedbackExResponse(exResId, feedbackItem);
            if (check)
            {
                return Ok("Thank you for your feedback!");
            }
            else
            {
                return BadRequest("Feedback fail!");
            }
        }

        [HttpGet("GetFeedbacksByType")]
        public async Task<IActionResult> GetFeedbacksByType(FeedbackType type)
        {
            var result = await _feedbackService.GetFeedbacksByType(type);
            return Ok(result);
        }

        [HttpGet("GetFeedbackByType")]
        public async Task<IActionResult> GetFeedbackByType(string id)
        {
            var result = await _feedbackService.GetFeedbackByType(id);
            return Ok(result);
        }

        [HttpGet("GetUserFeedbacks")]
        public async Task<IActionResult> GetUserFeedbacks(int userId)
        {
            var result = await _feedbackService.GetUserFeedbacks(userId);
            return Ok(result);
        }

        [HttpGet("GetFeedbackById")]
        public async Task<IActionResult> GetFeedbackById(string id)
        {
            var result = await _feedbackService.GetFeedbackById(id);
            return Ok(result);
        }

        [HttpPut("ReplyFeedback")]
        public async Task<IActionResult> ReplyFeedback(string id, [FromBody] ReplyFeedbackItem item)
        {
            bool check = await _feedbackService.ReplyFeedback(id, item);
            if (check)
            {
                return Ok("Reply feedback successfully!");
            }
            else
            {
                return BadRequest("Reply feedback fail!");
            }
        }
    }
}
