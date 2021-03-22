using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace DataAccess
{
    public class PostRepository : IPostRepository
    {
        public Task<IEnumerable<Post>> GetAllPostsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CreatePostAsync(Post post)
        {
            throw new System.NotImplementedException();
        }

        public Task<Post> GetPostByIdAsync(int postId)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdatePostAsync(Post postToUpdate)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeletePostAsync(int postId)
        {
            throw new System.NotImplementedException();
        }
    }
}