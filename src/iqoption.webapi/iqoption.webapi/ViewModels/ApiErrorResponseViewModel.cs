using System.Net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace iqoption.webapi.ViewModels
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