using Infrastructure.DTOs;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Services
{
    public class MemberService(IJsonRepository jsonRepository, IMemberMapper memberMapper) : IMemberService
    {
        private IJsonRepository _jsonRepository = jsonRepository;
        private readonly List<Member> _members = [];
        private readonly IMemberMapper _memberMapper = memberMapper;
        private bool _loaded;

        public async Task<ResponseResult> EnsureLoadedAsync() 
        {
            if (_loaded)
                return new ResponseResult { Success = true };

            ResponseResult<IEnumerable<Member>> result = await _jsonRepository.GetContentFromFile();
            if (!result.Success || result.Data is null)
            {
                return new ResponseResult { Success = false, Message = "Unknown error occurred while loading data"};
            }
            _members.AddRange(result.Data);
            _loaded = true;

            return new ResponseResult { Success = true};
        }


        public async Task<ResponseResult<bool>> DeleteMemberAsync(string ssn)
        {
            try
            {
                await EnsureLoadedAsync();

                var removedCount = _members.RemoveAll(e => e.SocialSecurityNumber == ssn);
                if (removedCount > 0)
                {
                    await _jsonRepository.SaveContentToFileAsync(_members);
                    return new ResponseResult<bool>
                    {
                        Success = true,
                        Message = "Member deleted",
                    };
                }
            }
            catch (Exception)
            {

                return new ResponseResult<bool>
                {
                    Success = false,
                    Message = "Could not delete user",
                };
            }


            return new ResponseResult<bool>
            {
                Success = false,
                Message = "Could not delete user",
            };


        }

        public async Task<ResponseResult<IEnumerable<Member>>> GetAllMembersAsync()
        {
           ResponseResult<IEnumerable<Member>> loadResult = await _jsonRepository.GetContentFromFile();

            if (!loadResult.Success)
            {
                return new ResponseResult<IEnumerable<Member>>
                {
                    Success = false,
                    Message = loadResult.Message,
                    Data = []
                };
            }

            return new ResponseResult<IEnumerable<Member>>
            {
                Success = true,
                Data = loadResult.Data
            };
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

            if (string.IsNullOrWhiteSpace(member.PostalCode) || member.PostalCode.Length != 5 || !member.PostalCode.All(char.IsDigit))
            {
                return new ResponseResult
                {
                    Success = false,
                    Message = "Ogiltigt postnummer (måste vara 5 siffror)."
                };
            }

            if (member.TermsAccepted == false)
            {
                return new ResponseResult
                {
                    Success = false,
                    Message = "Medlemmen måste acceptera villkoren."
                };
            }

            await EnsureLoadedAsync();

            _members.Add(member);
            await _jsonRepository.SaveContentToFileAsync(_members);

            return new ResponseResult
            {
                Success = true,
                Message = "Medlemmen har sparats."
            };
        }
        public async Task<ResponseResult> UpdateMemberAsync(MemberUpdateRequest updateRequest)
        {
            if (updateRequest is null)
                return new ResponseResult
                {
                    Success = false,
                    Message = "Invalid update request"
                };

            await EnsureLoadedAsync();

            var existing = _members.FirstOrDefault(m => m.SocialSecurityNumber == updateRequest.SocialSecurityNumber);
            if (existing == null)
                return new ResponseResult
                {
                    Success = false,
                    Message = "Member not found"
                };

            try
            {
                _memberMapper.MapFromUpdateRequestToMember(existing, updateRequest);
                await _jsonRepository.SaveContentToFileAsync(_members);

                return new ResponseResult
                {
                    Success = true,
                    Message = "Member updated successfully"
                };
            }
            catch (Exception ex)
            {
                return new ResponseResult
                {
                    Success = false,
                    Message = $"Failed to update member: {ex.Message}"
                };
            }
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
