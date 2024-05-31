
using Forum.Contracts;
using Forum.Data;
using Forum.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context) 
        {
             _context = context;
        }
        public async Task AddPostAsync(Post entity)
        {
            if (entity != null)
            {
                await _context.Posts.AddAsync(entity);
            }
        }

        public void DeletePost(Post entity)
        {
            if (entity != null)
            {
                _context.Posts.Remove(entity);
            }
        }

        public async Task<List<Post>> GetAllPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<List<Post>> GetAllPostsAsync(Expression<Func<Post, bool>> filter)
        {
            return await _context.Posts.Where(filter).ToListAsync();
        }

        public async Task<Post> GetSinglePostAsync(Expression<Func<Post, bool>> filter)
        {
            return await _context.Posts.FirstOrDefaultAsync(filter);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePostAsync(Post entity)
        {
            if(entity != null)
            {
                var result = await _context.Posts.FirstOrDefaultAsync(p => p.Id == entity.Id);

                if (result != null)
                {
                    result.Title = entity.Title;
                    result.Status = entity.Status;
                    result.Content = entity.Content;
                    result.CreatedAt = entity.CreatedAt;
                    result.State = entity.State;
                    result.Status = entity.Status;
                    result.Comments = entity.Comments;

                    _context.Posts.Update(result);
                }
            }
        }
    }
}
