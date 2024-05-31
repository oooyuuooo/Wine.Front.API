using Microsoft.AspNetCore.Mvc;
using Wine.BLL.Service.Members;
using Wine.DAL.EFRepository.Members;
using Wine.IDAL.IRepository.Members;
using Wine.Infa.Dto.Members;
using Wine.Infa.EFModel.Models;

namespace Wine.Front.API.Controllers.Members
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : Controller
    {
        private readonly IMemberRepository _repo;
        private readonly MemberService _service;

        public MembersController(AppDbContext context)
        {
            _repo = new MemberEFRepository(context);
            _service = new MemberService(_repo);
        }

        [HttpPost("Login")]
        public async Task<bool> Login([FromQuery] MemberLoginDto dto)
        {
            var result = _service.Login(dto.Account, dto.Password);
            return result;
        }

        [HttpPost("Register")]
        public async Task<bool> Register([FromQuery] MemberRegisterDto dto)
        {
            var result = _service.Register(dto);
            return result;
        }
    }
}
