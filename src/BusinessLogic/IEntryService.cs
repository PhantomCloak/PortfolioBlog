using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.V1.Commands;
using Shared.Contracts.V1.Queries;
using Shared.DTOs;

namespace BusinessLogic
{
    public interface IEntryService
    {
        Task<IEnumerable<EntryDto>> GetAllEntriesAsync();
        Task<EntryDto> GetEntryAsync(EntryQuery entryQuery);
        Task<EntryDto> CreateEntryAsync(EntryCommand entryCommand);
        Task<bool> DeleteEntryAsync(EntryQuery entryQuery);
    }
}