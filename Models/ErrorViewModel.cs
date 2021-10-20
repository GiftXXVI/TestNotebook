using System;

namespace TestNotebook.Models
{
    public class ErrorViewModel
    {
        public string RequestId { get; set; }
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public string Message { get; set; }
        public int Id { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }

    public class SuccessViewModel
    {
        public int StatusCode { get; set; }
        public string StatusMessage { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
        public int Id { get; set; }
    }
}
