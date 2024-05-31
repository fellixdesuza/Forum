using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;
        private ApiResponse _response;
        public PostsController(IPostService postService)
        {
            _postService = postService;
            _response = new ApiResponse();
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> AllPostsOfUser([FromRoute] string userId)
        {
            var result = await _postService.GetAllPostAsync(userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }
        [HttpGet("{userId:guid}/{postId:int}")]
        public async Task<IActionResult> SinglePostOfUser([FromRoute] string userId, [FromRoute] int postId)
        {
            var result = await _postService.GetSinglePostAsync(postId, userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPost]
        public async Task<IActionResult> AddPost([FromBody] PostForAddDto model)
        {
            await _postService.AddPostAsync(model);

            _response.Result = model;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.Created);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }


        [HttpDelete("{postId:int}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int postId)
        {
            await _postService.DeletePostAsync(postId);

            _response.Result = postId;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.NoContent);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }
    }
}
