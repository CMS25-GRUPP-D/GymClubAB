using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class MemberService : IMemberService
    {
        public Task<bool> DeleteMemberAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Member>> GetAllMemberAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Member> GetMemberByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveMemberAsync(Member member)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateMemberAsync(Member member)
        {
            throw new NotImplementedException();
        }
    }
}
