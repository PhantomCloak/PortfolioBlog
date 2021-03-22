using System.ComponentModel.DataAnnotations;

namespace Shared.Contracts.V1.Queries
{
    public class ContentQuery
    {
        [Required]
        public string ContentName { get; set; }
    }
}