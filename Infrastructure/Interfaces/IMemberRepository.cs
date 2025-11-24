using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Models;

namespace Infrastructure.Interfaces
{
    public interface IMemberRepository
    {
        Task<IEnumerable<Member>> GetAllAsync();
        Task<Member?> GetBySsnAsync(string ssn);
        Task AddAsync(Member member);
        Task UpdateAsync(Member member);
        Task<bool> DeleteAsync(string ssn);
    }
}

