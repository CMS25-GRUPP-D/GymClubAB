using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class MemberService(IJsonRepository jsonRepository) : IMemberService
    {
        private IJsonRepository _jsonRepository = jsonRepository;
        private readonly List<Member> _members = [];

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

        public async Task<ResponseResult> SaveMemberAsync(Member member)
        {

            if (member == null)
            {
                return new ResponseResult
                {
                    Success = false,
                    Message = "Otillräcklig data för medlem."
                };
            }

            if (string.IsNullOrWhiteSpace(member.SocialSecurityNumber))
            {
                return new ResponseResult
                {
                    Success = false,
                    Message = "Personnummer saknas i medlemsdata."
                };
            }

            if (!IsValidPersonNumber(member.SocialSecurityNumber))
            {
                return new ResponseResult
                {
                    Success = false,
                    Message = "Ogiltigt personnummer format."
                };
            }

            _members.Add(member);
            await _jsonRepository.SaveContentToFileAsync(_members); 



            return new ResponseResult
            {
                Success = true,
                Message = "Medlemmen har sparats."
            };
        }

        public Task<bool> UpdateMemberAsync(Member member)
        {
            throw new NotImplementedException();
        }

        private static bool IsValidPersonNumber(string personalNumber)
        {
            if (string.IsNullOrWhiteSpace(personalNumber))
                return false;

            string cleanNumber = personalNumber.Replace("-", "").Replace(" ", "");

            if (cleanNumber.Length != 10 && cleanNumber.Length != 12)
                return false;

            return cleanNumber.All(char.IsDigit);
        }
    }
}
