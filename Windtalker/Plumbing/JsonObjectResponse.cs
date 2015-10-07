using Nancy.Responses;

namespace Windtalker.Plumbing
{
    public class JsonObjectResponse : JsonResponse
    {
        public JsonObjectResponse(object obj)
            : base(obj, new DefaultJsonSerializer())
        {
        }
    }
}