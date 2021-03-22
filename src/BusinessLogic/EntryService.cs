using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.V1.Commands;
using Shared.Contracts.V1.Queries;
using Shared.DTOs;

namespace BusinessLogic
{
    public class EntryService : IEntryService
    {
        public Task<IEnumerable<EntryDto>> GetAllEntriesAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<EntryDto> GetEntryAsync(EntryQuery entryQuery)
        {
            throw new System.NotImplementedException();
        }

        public Task<EntryDto> CreateEntryAsync(EntryCommand entryCommand)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteEntryAsync(EntryQuery entryQuery)
        {
            throw new System.NotImplementedException();
        }
    }
}