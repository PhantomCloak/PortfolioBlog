using System.Collections.Generic;

namespace Shared.Models
{
    public class Content
    {
        public int ContentId { get; set; }
        public string ContentName { get; set; }
        public Dictionary<string, string> ContentFields { get; set; }
    }
}