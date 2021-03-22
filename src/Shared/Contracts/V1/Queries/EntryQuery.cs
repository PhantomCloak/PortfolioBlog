using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.V1.Queries
{
    public class EntryQuery
    {
        public string EntryId { get; set; }
        public string EntryName { get; set; }
        public string EntryCategory { get; set; }
    }
}