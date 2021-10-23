using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogApi.Data;
using BlogApi.Models;

namespace BlogApi.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly DotNetCoreMySQLContext _db;
        public PostRepository(DotNetCoreMySQLContext db)
        {
            if (db == null) throw new ArgumentNullException("DBContext invalido");
            _db = db;
        }

        public async Task<bool> AddPost(Post post)
        {
            try
            {
                await _db.Post.AddAsync(post);
                await _db.SaveChangesAsync();
                return true;
            }
            catch// (Exception ex)
            {
                return false;
            }
        }

        public async Task<int> DeletePost(int? id)
        {
            int result = 0;
            var post = await _db.Post.FirstOrDefaultAsync(x => x.Id == id);
            if (post != null)
            {
                _db.Post.Remove(post);
                result = await _db.SaveChangesAsync();
            }
            return result;
        }

        public async Task<Post> GetPost(int? id)
        {
            return await _db.Post.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Post>> GetPosts()
        {
            return await _db.Post.ToListAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _db.Post.Update(post);
            await _db.SaveChangesAsync();
        }
    }
}
