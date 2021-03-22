using Dapper.FluentMap.Mapping;
using Shared.Models;

namespace DataAccess.Maps
{
    public class ContentMap : EntityMap<Content>
    {
        public ContentMap()
        {
            Map(p => p.ContentId).ToColumn("content_id");
            Map(p => p.ContentName).ToColumn("content_name");
            Map(p => p.ContentFields).ToColumn("content_fields");
        }
    }
}