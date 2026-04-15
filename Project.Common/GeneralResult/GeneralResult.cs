using System.Diagnostics;
using System.Text.Json.Serialization;

namespace Project.Common
{
    public class GeneralResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Dictionary<string, List<Error>>? Errors { get; set; }

        public static GeneralResult SuccessResult(string message = "Success")
            => new() { Success = true, Message = message, Errors = null };

        public static GeneralResult NotFound(string message = "Resource Not Found")
            => new() { Success = false, Message = message, Errors = null };

        public static GeneralResult FailRequest(string message = "Operation Failed")
            => new() { Success = false, Message = message, Errors = null };

        public static GeneralResult FailResult(Dictionary<string, List<Error>>? errors, string message = "One or more errors occurred")
            => new() { Success = false, Message = message, Errors = errors };

    }

    public class GeneralResult<T> : GeneralResult
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }
        public static GeneralResult<T> SuccessResult(T data, string message = "Success")
            => new() { Success = true, Message = message, Data = data, Errors = null };
        public static new GeneralResult<T> NotFound(string message = "Resource Not Found")
            => new() { Success = false, Message = message, Data = default, Errors = null };
        public static new GeneralResult<T> FailRequest(string message = "Operation Failed")
            => new() { Success = false, Message = message, Data = default, Errors = null };
        public static new GeneralResult<T> FailResult(Dictionary<string, List<Error>>? errors, string message = "One or more errors occurred")
            => new() { Success = false, Message = message, Data = default, Errors = errors };
    }
}
