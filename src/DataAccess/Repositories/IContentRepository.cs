using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Models;

namespace DataAccess.Repositories
{
    public interface IContentRepository
    {
        Task<IEnumerable<Content>> GetAllContentsAsync();
        Task<bool> CreateContentAsync(Content content);
        Task<Content> GetContentByKeyAsync(string key);
        Task<bool> UpdateContentAsync(Content contentToUpdate);
        Task<bool> DeleteContentAsync(string contentKey);
    }
}