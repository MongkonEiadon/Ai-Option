using System;

namespace ai.option.web.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public string ErrorMessage { get; set; }
        public Exception Exception { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}