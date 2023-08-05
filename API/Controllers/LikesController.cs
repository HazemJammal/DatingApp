using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class LikesController : BaseApiController
    {
        private readonly IUserRepository _userRepository;
        private readonly ILikeRepository _likeRepository;
        public LikesController(IUserRepository userRepository, ILikeRepository likeRepository)
        {
            this._likeRepository = likeRepository;
            this._userRepository = userRepository;
        }

        [HttpPost("{username}")]

        public async Task<ActionResult> AddLike(string username)
        {
            var sourceUserId = User.GetUserId();
            var targetUser = await _userRepository.GetUserByUsernameAsync(username);
            var sourceUser = await _likeRepository.GetUserWithLike(sourceUserId);

            if (targetUser == null) return NotFound();

            if (sourceUser.UserName == username) return BadRequest("You cannot like yourself werido");

            //Check if user already like the target user
            var userLike = await _likeRepository.GetUserLike(sourceUserId, targetUser.Id);

            if (userLike != null) return BadRequest("You already like this person");

            userLike = new UserLike
            {
                SourceUserId = sourceUserId,
                TargetUserId = targetUser.Id
            };
            //Adding target user to source user likes
            sourceUser.LikedUsers.Add(userLike);

            if (await _userRepository.SaveAllAsync()) return Ok();

            return BadRequest("Failed to like user");
        }
        [HttpGet]

        public async Task<ActionResult<PagedList<LikeDto>>> GetUserLikes([FromQuery] LikeParams likeParams)
        {
            likeParams.UserId = User.GetUserId();

            var users = await _likeRepository.GetUserLikes(likeParams);
            Response.AddPaginationHeader(new PaginationHeader(users.CurrentPage, users.TotalCount
         , users.TotalPages, users.PageSize));
            return Ok(users);
        }
    }
}