using System.Collections.Generic;

namespace Shared.DTOs
{
    public class ContentDto
    {
        public string ContentName { get; set; }
        public Dictionary<string, string> ContentFields { get; set; }
    }
}