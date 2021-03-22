using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.V1.Commands
{
    public class EntryCommand
    {
        [Required]
        public string CategoryName { get; set; }
        [Required]
        public string PostName { get; set; }
        [Required]
        public string Content { get; set; }
        [Required]
        public IEnumerable<string> Categories { get; set; }
    }
}