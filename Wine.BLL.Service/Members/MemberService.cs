using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.IDAL.IRepository.Members;
using Wine.Infa.Dto.Members;
using Wine.Infa.Entity.Members;

namespace Wine.BLL.Service.Members
{
    public class MemberService
    {
        private readonly IMemberRepository _repo;

        public MemberService(IMemberRepository repo)
        {
            _repo = repo;
        }

        public bool Login(string account, string password)
        {
            //判斷帳號密碼
            if (string.IsNullOrEmpty(account) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            var entity = _repo.Login(account, password);
            if (entity == null)
            {
                return false;
            }

            return true;
        }

        public bool Register(MemberRegisterDto dto)
        {
            MemberRegisterEntity entity = new MemberRegisterEntity
            {
                Id = dto.Id,
                Account = dto.Account,
                Password = dto.Password,
                Name = dto.Name,
                Email = dto.Email,
                Phone = dto.Phone,
                DateOfBirth = dto.DateOfBirth
            };

            //判斷帳號是否存在
            var isExist = _repo.GetMember(entity.Account);

            if (isExist != null)
            {
                var result = _repo.Register(entity);
                if (result != null)
                {
                    return true;
                }
                return false;
            }

            return false;
        }
    }
}
