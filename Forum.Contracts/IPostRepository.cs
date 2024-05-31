using Forum.Entities;
using System.Linq.Expressions;
namespace Forum.Contracts
{
    public interface IPostRepository : ISavable
    {
        Task<List<Post>> GetAllPostsAsync();
        Task<List<Post>> GetAllPostsAsync(Expression<Func<Post, bool>> filter);
        Task<Post> GetSinglePostAsync(Expression<Func<Post, bool>> filter);
        Task AddPostAsync(Post entity);
        Task UpdatePostAsync(Post entity);
        void DeletePost(Post entity);
    }
}
