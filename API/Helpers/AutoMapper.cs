using API.DTO;
using API.Entities;
using API.Extensions;
using AutoMapper;

namespace API.Helpers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<AppUser,MemberDTO>()
            .ForMember(dest => dest.PhotoURL,
                 opt => opt.MapFrom(src => src.Photos.FirstOrDefault(x => x.IsMain).Url))
                 .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.DateOfBirth.CalculateAge()));
            CreateMap<Photo,PhotoDTO>();
        }        
    }
}