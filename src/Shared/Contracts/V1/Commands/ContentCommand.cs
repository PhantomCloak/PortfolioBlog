using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.V1.Commands
{
    public class ContentCommand
    {
        [Required]
        [StringLength(maximumLength:32,ErrorMessage = "Content Name cannot be longer than 32 characters or shorter than 3 characters.",MinimumLength = 3)]
        public string ContentName { get; set; }
        public Dictionary<string,string> ContentFields { get; set; }
    }
}