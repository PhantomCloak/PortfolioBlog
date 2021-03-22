using System.Collections.Generic;

namespace Shared.DTOs
{
    public class EntryDto
    {
        public CategoryDto Category { get; set; }
        public IEnumerable<PostDto> Posts { get; set; } 
    }
}