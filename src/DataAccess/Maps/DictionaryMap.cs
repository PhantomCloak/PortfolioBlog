using System.Collections.Generic;
using System.Data;
using Dapper;
using Newtonsoft.Json;

namespace DataAccess.Maps
{
    public class ContentParseMap : SqlMapper.TypeHandler<Dictionary<string,string>>
    {
        public override void SetValue(IDbDataParameter parameter, Dictionary<string, string> value)
        {
            parameter.Value = value != null ? JsonConvert.SerializeObject(value) : string.Empty;
        }

        public override Dictionary<string, string> Parse(object value)
        {
            return value != null ? JsonConvert.DeserializeObject<Dictionary<string, string>>(value.ToString()!) : null;
        }
    }
}