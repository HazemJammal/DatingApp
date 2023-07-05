using API.Entities;
using API.Intefaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using API.DTO;

namespace API.Controllers
{
    [Authorize]
    public class UsersController : BaseApiController
    {
        private readonly IUserRepository _reposoitory;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository reposoitory, IMapper mapper)
        {
            _reposoitory = reposoitory;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers()
        {
            var users = await _reposoitory.GetUsersAsync();

            var usersToReturn = _mapper.Map<IEnumerable<MemberDTO>>(users);
            return Ok(usersToReturn);
        }
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDTO>> GetUser(string username )
        {
            username=username.ToLower();
            return await _reposoitory.GetMemberByNameAsync(username);
        }
    }
}