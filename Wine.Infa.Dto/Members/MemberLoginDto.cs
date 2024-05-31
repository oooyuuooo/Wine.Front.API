using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wine.Infa.Dto.Members
{
    public class MemberLoginDto
    {
        public int? Id { get; set; }
        public string Account { get; set; }
        public string Password { get; set; }
    }
}
