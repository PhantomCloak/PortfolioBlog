using System.Collections.Generic;
using System.Threading.Tasks;
using Shared.Contracts.V1.Commands;
using Shared.DTOs;

namespace BusinessLogic
{
    public class EntryService : IEntryService
    {
        public Task<IEnumerable<EntryDto>> GetAllBrandsAsync()
        {
            throw new System.NotImplementedException();
        }

        public Task<EntryDto> GetBrandAsync(string brandName)
        {
            throw new System.NotImplementedException();
        }

        public Task<EntryDto> CreateEntryAsync(EntryCommand createBrandParams)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> DeleteEntryAsync(string brandName)
        {
            throw new System.NotImplementedException();
        }
    }
}