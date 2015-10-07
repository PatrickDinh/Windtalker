using Nancy;
using Nancy.Responses;
using Newtonsoft.Json;

namespace Windtalker.Plumbing
{
    public class ErrorResponse : JsonResponse
    {
        readonly Error error;

        private ErrorResponse(Error error)
            : base(error, new DefaultJsonSerializer())
        {
            this.error = error;
        }

        public static ErrorResponse FromMessage(string message, HttpStatusCode statusCode)
        {
            var response = new ErrorResponse(new Error { ErrorMessage = message });
            response.StatusCode = statusCode;
            return response;
        }

        public static ErrorResponse FromMessage(string[] errors, HttpStatusCode statusCode)
        {
            var response = new ErrorResponse(new Error { Errors = errors });
            response.StatusCode = statusCode;
            return response;
        }

        class Error
        {
            public string ErrorMessage { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string FullException { get; set; }

            [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
            public string[] Errors { get; set; }
        }
    }
}