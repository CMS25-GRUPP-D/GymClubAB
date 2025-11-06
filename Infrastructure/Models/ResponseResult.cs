using Infrastructure.Interfaces;

namespace Infrastructure.Models;

public class ResponseResult : IResponseResult
{
    public bool Success { get; set; }
    public string? Message { get; set; }
}

public class ResponseResult<T> : ResponseResult
{
    public T? Data { get; set; }

}
