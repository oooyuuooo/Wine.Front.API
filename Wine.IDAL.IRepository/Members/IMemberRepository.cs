using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wine.Infa.Entity.Members;

namespace Wine.IDAL.IRepository.Members
{
    public interface IMemberRepository
    {
        int Register(MemberRegisterEntity entity);
        MemberLoginEntity Login(string account, string password);
        MemberRegisterEntity GetMember(string account);
        void Update(MemberRegisterEntity entity);
    }
}
