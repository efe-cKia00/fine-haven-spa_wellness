using System.Collections.Generic;

namespace Shared.Services
{
    public class OperationResult
    {
        public bool Success { get; init; }
        public IReadOnlyList<string> Errors { get; init; } = new List<string>();

        public static OperationResult Ok() => new() { Success = true, Errors = new List<string>() };
        public static OperationResult Fail(params string[] errors) => new() { Success = false, Errors = errors };
    }
}