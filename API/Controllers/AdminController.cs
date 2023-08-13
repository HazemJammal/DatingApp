using API.Data;
using API.DTOs;
using API.Entities;
using API.Helpers;
using API.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AdminController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;

        public AdminController(UserManager<AppUser> userManager, DataContext dataContext, IMapper mapper,
            IUserRepository userRepository
        )
        {
            this._mapper = mapper;
            this._context = dataContext;
            this._userManager = userManager;
            this._userRepository = userRepository;

        }
        [Authorize(Policy = "RequireAdminRole")]
        [HttpGet("users-with-roles")]
        public async Task<ActionResult> GetUsersWithRoles()
        {

            var users = await _userManager.Users
                        .OrderBy(u => u.UserName)
                        .Select(u => new
                        {
                            u.Id,
                            username = u.UserName,
                            Roles = u.UserRoles.Select(r => r.Role.Name).ToList()
                        })
                        .ToListAsync();

            return Ok(users);
        }

        [Authorize(Policy = "RequireAdminRole")]
        [HttpPut("edit-roles/{username}")]
        public async Task<ActionResult> EditUserRoles(string username, [FromQuery] string roles)
        {

            if (string.IsNullOrEmpty(roles)) return BadRequest("Select at least one roles");

            var selectedRoles = roles.Split(",").ToArray();

            var user = await _userManager.FindByNameAsync(username);

            if (user == null) return NotFound();

            var userRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.AddToRolesAsync(user, selectedRoles.Except(userRoles));

            if (!result.Succeeded) return BadRequest(result.Errors);

            result = await _userManager.RemoveFromRolesAsync(user, userRoles.Except(selectedRoles));

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok(await _userManager.GetRolesAsync(user));

        }


        [Authorize(Policy = "RequireModeratorRole")]
        [HttpGet("photos-to-moderate")]
        public async Task<ActionResult<IEnumerable<PhotosToVerifyDto>>> GetPhotosAsModerator()
        {
            var photos = await _context.Photos
                                .Include(p => p.AppUser)
                                .Where(p => p.ShowAdminPhoto)
                                .ToListAsync();

            var photosDto = photos.Select(photo => new PhotosToVerifyDto
            {
                Id = photo.Id,
                Url = photo.Url,
                Username = photo.AppUser.UserName
            });


            return Ok(photosDto);
        }

        [Authorize(Policy = "RequireModeratorRole")]
        [HttpPut("photos-to-verify/{id}")]

        public async Task<ActionResult> VerifyPhoto(int id, [FromQuery] string decision)
        {


            var photo = await _context.Photos.Include(x => x.AppUser).FirstOrDefaultAsync(x => x.Id == id);
            var user = await _userRepository.GetMemberAsync(photo.AppUser.UserName);

            if (photo == null) return NotFound();

            if (photo.ShowAdminPhoto == false) return BadRequest("Photo is already verified");

            photo.ShowAdminPhoto = false;

            if (decision == "Accept")
            {
                photo.AllowPhoto = true;
                if (user.Photos.Count == 0)
                {
                    photo.IsMain = true;
                }

            }
            else if (decision == "Reject")
            {
                photo.AllowPhoto = false;
            }

            if (await _context.SaveChangesAsync() > 0) return Ok(user);

            return BadRequest("Something Went worng");
        }


    }
}