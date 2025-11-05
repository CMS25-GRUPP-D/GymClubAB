namespace Infrastructure.Interfaces;

public interface IResponseResult
{
    string Message { get; set; }
    bool Success { get; set; }
}
