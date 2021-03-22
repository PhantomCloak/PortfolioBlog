using System.Collections.Generic;

namespace Shared.DTOs
{
    public class CategoryDto
    {
        public string CategoryName { get; set; }
        public IEnumerable<PostDto> Posts { get; set; }
    }
}