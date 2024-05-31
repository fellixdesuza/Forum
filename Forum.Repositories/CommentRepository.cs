using Forum.Contracts;
using Forum.Data;
using Forum.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Forum.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task AddCommentAsync(Comment entity)
        {
            if (entity != null)
            {
                await _context.Comments.AddAsync(entity);
            }
        }

        public void DeleteComment(Comment entity)
        {
            if (entity != null)
            {
                _context.Comments.Remove(entity);
            }
        }

        public async Task<List<Comment>> GetAllCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<List<Comment>> GetAllCommentsAsync(Expression<Func<Comment, bool>> filter)
        {
            return await _context.Comments.Where(filter).ToListAsync();
        }

        public async Task<Comment> GetSingleCommentAsync(Expression<Func<Comment, bool>> filter)
        {
            return await _context.Comments.FirstOrDefaultAsync(filter);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCommentAsync(Comment entity)
        {
            if (entity != null)
            {
                var result = await _context.Comments.FirstOrDefaultAsync(p => p.Id == entity.Id);

                if (result != null)
                {
                    result.Content = entity.Content;
                    result.CreatedAt = entity.CreatedAt;
                    result.Content = entity.Content;
                    result.Post = entity.Post;
                   

                    _context.Comments.Update(result);
                }
            }
        }
    }
}
