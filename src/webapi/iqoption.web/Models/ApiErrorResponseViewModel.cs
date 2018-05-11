using System.Net;
using Newtonsoft.Json;

namespace iqoption.web.Models
{
    public class ApiErrorResponseViewModel
    {
        [JsonProperty("code")]
        public HttpStatusCode Code { get; }

        [JsonProperty("name")]
        public string StatusName { get; }

        [JsonProperty("message")]
        public string Message { get; }

        public ApiErrorResponseViewModel(HttpStatusCode code, string message)
        {
            Code = code;

            StatusName = code.ToString();

            Message = message;
        }

        public static ApiErrorResponseViewModel UnAuthorizationMessage => new ApiErrorResponseViewModel(HttpStatusCode.Unauthorized, "Given login information not authorized!");
    }
}