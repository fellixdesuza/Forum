using Forum.Contracts;
using Forum.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Forum.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private ApiResponse _response;
        public CommentsController(ICommentService commentService) 
        {
            _commentService = commentService;
            _response = new ApiResponse();
        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> AllCommentsOfUser([FromRoute] string userId)
        {
            var result = await _commentService.GetAllCommentAsync(userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }
        [HttpGet("{userId:guid}/{commentId:int}")]
        public async Task<IActionResult> SingleTodoOfUser([FromRoute] string userId, [FromRoute] int commentId)
        {
            var result = await _commentService.GetSingleCommentAsync(commentId, userId);

            _response.Result = result;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.OK);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }

        [HttpPost]
        public async Task<IActionResult> AddComment([FromBody] CommentForAddDto model)
        {
            await _commentService.AddCommentAsync(model);

            _response.Result = model;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.Created);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }


        [HttpDelete("{commentId:int}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int commentId)
        {
            await _commentService.DeleteCommentAsync(commentId);

            _response.Result = commentId;
            _response.IsSuccess = true;
            _response.StatusCode = Convert.ToInt32(HttpStatusCode.NoContent);
            _response.Message = "Request completed successfully";

            return StatusCode(_response.StatusCode, _response);
        }
    }
}
