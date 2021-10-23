using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApi.Models;

namespace BlogApi.Repository
{
    public interface IPostRepository
    {
        Task<List<Post>> GetPosts();
        Task<Post> GetPost(int? id);
        Task<bool> AddPost(Post post);
        Task<int> DeletePost(int? id);
        Task UpdatePost(Post post);
    }
}
