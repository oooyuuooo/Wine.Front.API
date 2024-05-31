using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.IDAL.IRepository.Members;
using Wine.Infa.EFModel.Models;
using Wine.Infa.Entity.Members;

namespace Wine.DAL.EFRepository.Members
{
    public class MemberEFRepository : IMemberRepository
    {
        private AppDbContext _db;

        public MemberEFRepository(AppDbContext db)
        {
            _db = db;
        }

        public MemberRegisterEntity GetMember(string account)
        {
            var member = _db.Members
                            .Where(m => m.Account == account)
                            .FirstOrDefault();
            if (member == null)
            {
                return null;
            }
            var entity = new MemberRegisterEntity
            {
                Id = member.Id,
                Account = member.Account,
                Password = member.Password
            };
            return entity;
        }

        public MemberLoginEntity Login(string account, string password)
        {
            var normalizedAccount = account.ToUpper();
            var normalizedPassword = password.ToUpper();

            var member = _db.Members
                .Where(m => m.Account.ToUpper() == normalizedAccount && m.Password.ToUpper() == normalizedPassword)
                .FirstOrDefault();

            if (member == null)
            {
                return null;
            }

            var entity = new MemberLoginEntity
            {
                Id = member.Id,
                Account = member.Account,
                Password = member.Password
            };
            return entity;
        }

        public int Register(MemberRegisterEntity entity)
        {
            Member member = new Member
            {
                Id = entity.Id,
                Name = entity.Name,
                Account = entity.Account,
                Password = entity.Password,
                Email = entity.Email,
                Phone = entity.Phone,
                DateOfBirth = entity.DateOfBirth
            };

            _db.Members.Add(member);
            _db.SaveChanges();
            return member.Id;
        }

        public void Update(MemberRegisterEntity entity)
        {
            var member = _db.Members
                .Where(m => m.Id == entity.Id)
                .FirstOrDefault();

            member.Name = entity.Name;
            member.Password = entity.Password;
            member.Email = entity.Email;
            member.Phone = entity.Phone;
            member.DateOfBirth = entity.DateOfBirth;
            _db.SaveChanges();
        }
    }
}
