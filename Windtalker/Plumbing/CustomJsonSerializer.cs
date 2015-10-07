using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Windtalker.Plumbing
{
    public class CustomJsonSerializer : JsonSerializer
    {
        public CustomJsonSerializer()
        {
            Converters.Add(new StringEnumConverter
            {
                AllowIntegerValues = false,
                CamelCaseText = true
            });
            Formatting = Formatting.Indented;
        }
    }
}