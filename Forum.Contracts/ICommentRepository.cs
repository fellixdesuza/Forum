using Forum.Entities;
using System.Linq.Expressions;

namespace Forum.Contracts
{
    public interface ICommentRepository : ISavable
    {
        Task<List<Comment>> GetAllCommentsAsync();
        Task<List<Comment>> GetAllCommentsAsync(Expression<Func<Comment, bool>> filter);
        Task<Comment> GetSingleCommentAsync(Expression<Func<Comment, bool>> filter);
        Task AddCommentAsync(Comment entity);
        Task UpdateCommentAsync(Comment entity);
        void DeleteComment(Comment entity);
    }
}
