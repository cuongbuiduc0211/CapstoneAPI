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
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly PostService _postService;
        public PostController(PostService postService)
        {
            _postService = postService;
        }

        [HttpPost("CreateNewPost")]
        public async Task<IActionResult> CreateNewPost([FromBody] PostItem postItem)
        {
            bool check = await _postService.CreateNewPost(postItem);
            if (check)
            {
                return Ok("Create new post successfully!");
            }
            else
            {
                return BadRequest("This post is already existed!");
            }
        }

        [HttpGet("CountPostsByMonth")]
        public async Task<IActionResult> CountPostsByMonth(DateTime date)
        {
            var result = await _postService.CountPostsByMonth(date);
            return Ok(result);
        }

        [HttpGet("GetAllPosts")]
        public async Task<IActionResult> GetAllPosts()
        {
            var result = await _postService.GetAllPosts();
            return Ok(result);
        }


        [HttpGet("GetAllPostsByType")]
        public async Task<IActionResult> GetAllPostsByType(PostType postType)
        {
            var result = await _postService.GetAllPostsByType(postType);
            return Ok(result);
        }

        [HttpGet("GetAllPostsByKeyword")]
        public async Task<IActionResult> GetAllPostsByKeyword(string keyword)
        {
            var result = await _postService.GetAllPostsByKeyword(keyword);
            return Ok(result);
        }
        [HttpGet("GetAllPostsByBrand")]
        public async Task<IActionResult> GetAllPostsByBrand(string brandId)
        {
            var result = await _postService.GetAllPostsByBrand(brandId);
            return Ok(result);
        }
        [HttpGet("GetPostById")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var result = await _postService.GetPostById(id);
            return Ok(result);
        }

        [HttpPut("UpdatePost")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] PostItem postItem)
        {
            bool check = await _postService.UpdatePost(id, postItem);
            if (check)
            {
                return Ok("Update post successfully!");
            }
            else
            {
                return BadRequest("Update post fail!");
            }
        }

        [HttpPut("ChangePostStatus")]
        public async Task<IActionResult> ChangePostStatus(int id, PostStatus postStatus)
        {
            bool check = await _postService.ChangePostStatus(id, postStatus);
            if (check)
            {
                return Ok("Update post successfully!");
            }
            else
            {
                return BadRequest("Update post fail!");
            }
        }
    }
}
