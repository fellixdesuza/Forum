using Forum.Models;

namespace Forum.Contracts
{
    public interface IPostService
    {
        Task<List<PostForGetDto>> GetAllPostAsync(string userId);
        Task<PostForGetDto> GetSinglePostAsync(int postId, string userId);

        Task AddPostAsync(PostForAddDto post);
        Task UpdatePostAsync(PostForUpdateDto post);
        Task DeletePostAsync(int id);
    }
}
