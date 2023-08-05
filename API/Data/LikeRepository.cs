using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Helpers;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class LikeRepository : ILikeRepository
    {
        private readonly DataContext _context;

        public LikeRepository(DataContext context)
        {
            this._context = context;

        }
        public async Task<UserLike> GetUserLike(int sourceUserId, int targetUserId)
        {
            return await _context.Likes.FindAsync(sourceUserId, targetUserId);
        }

        public async Task<PagedList<LikeDto>> GetUserLikes(LikeParams likeParams)
        {
            var users = _context.Users.OrderBy(u => u.UserName).AsQueryable();

            var likes = _context.Likes.AsQueryable();

            if (likeParams.Predicate == "liked")
            {
                likes = likes.Where(x => x.SourceUserId == likeParams.UserId);
                users = likes.Select(like => like.TargetUser);
            }

            if (likeParams.Predicate == "likedBy")
            {
                likes = likes.Where(x => x.TargetUserId == likeParams.UserId);
                users = likes.Select(like => like.SourceUser);
            }
            

            
            var likedUsers =  users.Select(user => new LikeDto{
                UserName = user.UserName,
                KnownAs = user.KnownAs,
                City = user.City,
                Id = user.Id,
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain).Url,
                Age = user.DateOfBirth.CalcuateAge()
            });

            return await  PagedList<LikeDto>
            .CreateAsync(likedUsers,likeParams.PageNumber,likeParams.PageSize);
        }

        public async Task<AppUser> GetUserWithLike(int userId)
        {
            return await _context.Users
                .Include(x => x.LikedUsers)
                .FirstOrDefaultAsync(i => i.Id == userId);
        }
    }
}