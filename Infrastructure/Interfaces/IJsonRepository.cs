using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IJsonRepository
{
    Task SaveContentToFileAsync(IEnumerable<Member> members);
    Task<ResponseResult<IEnumerable<Member>>> GetContentFromFile();

}
