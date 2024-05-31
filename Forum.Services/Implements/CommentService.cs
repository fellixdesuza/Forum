using AutoMapper;
using Forum.Contracts;
using Forum.Entities;
using Forum.Models;
using Forum.Services.Exeptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Forum.Services.Implements
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CommentService(ICommentRepository commentRepository, IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = Mapper.Initializer();
        }

        public async Task AddCommentAsync(CommentForAddDto comment)
        {
            if (comment == null) throw new ArgumentNullException("Invalid argument passed");

            if (comment.UserId.Trim() != AuthenticatedUserId().Trim())
                throw new UnauthorizedAccessException("Can't add different users comment");

            var result = _mapper.Map<Comment>(comment);
            await _commentRepository.AddCommentAsync(result);
            await _commentRepository.Save();
        }

        public async Task DeleteCommentAsync(int id)
        {
            if (id <= 0)
            { throw new ArgumentException("Invalid Argument"); }
            var result = await _commentRepository.GetSingleCommentAsync(c => c.Id == id);
            if (result == null) { throw new CommentNotFoundException(); }
            if (result.UserId.Trim() == AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() == "Admin")
            {
                _commentRepository.DeleteComment(result);
                await _commentRepository.Save();
            }
            else
            {
                throw new UnauthorizedAccessException("Can't delete different users comment.");
            }

        }

        public async Task<List<CommentForGetDto>> GetAllCommentAsync(string userId)
        {
            List<CommentForGetDto> result = new();
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("Invalid argument passed");

            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UserNotFoundException();
            var rawData = await _commentRepository.GetAllCommentsAsync(x => x.UserId.Trim() == userId.Trim());
            if (rawData.Count == 0) { throw new CommentNotFoundException(); }
            result = _mapper.Map<List<CommentForGetDto>>(rawData);
            return result;
        }

        public async Task<CommentForGetDto> GetSingleCommentAsync(int commentId, string userId)
        {
            if (commentId <= 0 || string.IsNullOrWhiteSpace(userId))
            { throw new ArgumentException("Invalid Argument"); }
            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UserNotFoundException();
            var rawData = await _commentRepository.GetSingleCommentAsync(c => c.Id == commentId && c.UserId == userId);
            if (rawData == null) { throw new CommentNotFoundException(); }
            var result = _mapper.Map<CommentForGetDto>(rawData);
            return result;
        }

        public async Task UpdateCommentAsync(CommentForUpdateDto comment)
        {
            if (comment is null)
            { throw new ArgumentException("Invalid Argument"); }
            var result = _mapper.Map<Comment>(comment);
            await _commentRepository.UpdateCommentAsync(result);
            await _commentRepository.Save();
        }
        private string AuthenticatedUserId()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
                return result;
            }
            else
            {
                throw new UnauthorizedAccessException("Can't get credentials of unauthorized user");
            }
        }
        private string AuthenticatedUserRole()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role);
                return result;
            }
            else
            {
                throw new UnauthorizedAccessException("Can't get credentials of unauthorized user");
            }
        }
    }
}
