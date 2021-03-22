using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.V1.Commands;
using Shared.Contracts.V1.Queries;
using Shared.DTOs;

namespace BusinessLogic.Services
{
    public interface IContentService
    {
        Task<IEnumerable<ContentDto>> GetAllContentAsync();
        Task<ContentDto> GetContentAsync(ContentQuery contentQuery);
        Task<ContentDto> CreateContentAsync(ContentCommand contentCommand);
        Task<bool> UpdateContentAsync(ContentDto contentDto);
        Task<bool> DeleteContentAsync(ContentQuery contentQuery);
    }
}