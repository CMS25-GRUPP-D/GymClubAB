using Infrastructure.Interfaces;

namespace Infrastructure.Models;

public class ResponseResult : IResponseResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}
