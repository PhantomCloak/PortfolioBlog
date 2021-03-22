using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.V1.Commands;
using Shared.DTOs;

namespace BusinessLogic
{
    public interface IEntryService
    {
        Task<IEnumerable<EntryDto>> GetAllBrandsAsync();
        Task<EntryDto> GetBrandAsync(string brandName);
        Task<EntryDto> CreateEntryAsync(EntryCommand createBrandParams);
        Task<bool> DeleteEntryAsync(string brandName);
    }
}