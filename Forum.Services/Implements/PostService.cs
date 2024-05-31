using AutoMapper;
using Forum.Contracts;
using Forum.Entities;
using Forum.Models;
using Forum.Services.Exeptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Forum.Services.Implements
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postrepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public PostService(IPostRepository postRepository, IHttpContextAccessor httpContextAccessor) 
        {
            _postrepository = postRepository;
            _httpContextAccessor = httpContextAccessor;
            _mapper = Mapper.Initializer();
        }
        public async Task AddPostAsync(PostForAddDto post)
        {
            if (post == null) throw new ArgumentNullException("Invalid argument passed");

            if (post.UserId.Trim() != AuthenticatedUserId().Trim())
                throw new UnauthorizedAccessException("Can't add different users comment");
            var result = _mapper.Map<Post>(post);
            await _postrepository.AddPostAsync(result);
            await _postrepository.Save();
        }

        public async Task DeletePostAsync(int id)
        {
            if (id <= 0)
            { throw new ArgumentException("Invalid Argument"); }
            var result = await _postrepository.GetSinglePostAsync(p => p.Id == id);
            if (result == null) {  throw new PostNotFoundException(); }
            if (result.UserId.Trim() == AuthenticatedUserId().Trim() || AuthenticatedUserRole().Trim() == "Admin")
            {
                _postrepository.DeletePost(result);
                await _postrepository.Save(); 
            }
            else
            {
                throw new UnauthorizedAccessException("Can't delete different users post.");
            }
        }

        public async Task<List<PostForGetDto>> GetAllPostAsync(string userId)
        {
            List<PostForGetDto> result = new();
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException("Invalid argument passed");

            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UserNotFoundException();
            var rawData = await _postrepository.GetAllPostsAsync();
            if(rawData.Count == 0) {  throw new PostNotFoundException(); }
            result = _mapper.Map<List<PostForGetDto>>(rawData);
            return result;
        }

        public async Task<PostForGetDto> GetSinglePostAsync(int postId, string userId)
        {
            if (postId <= 0 || string.IsNullOrWhiteSpace(userId))
            { throw new ArgumentException("Invalid Argument"); }
            if (AuthenticatedUserId().Trim() != userId.Trim())
                throw new UserNotFoundException();
            var rawData = await _postrepository.GetSinglePostAsync(p => p.Id == postId && p.UserId == userId);
            if (rawData == null) {  throw new PostNotFoundException(); }
            var result = _mapper.Map<PostForGetDto>(rawData);
            return result;
        }

        public async Task UpdatePostAsync(PostForUpdateDto post)
        {
            if (post is null)
            { throw new ArgumentException("Invalid Argument"); }
            var result = _mapper.Map<Post>(post);
            await _postrepository.UpdatePostAsync(result);
            await _postrepository.Save();
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
