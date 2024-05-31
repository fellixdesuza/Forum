using Forum.Models;

namespace Forum.Contracts
{
    public interface ICommentService
    {
        Task<List<CommentForGetDto>> GetAllCommentAsync(string userId);
        Task<CommentForGetDto> GetSingleCommentAsync(int commentId, string userId);

        Task AddCommentAsync(CommentForAddDto post);
        Task UpdateCommentAsync(CommentForUpdateDto post);
        Task DeleteCommentAsync(int id);
    }
}
