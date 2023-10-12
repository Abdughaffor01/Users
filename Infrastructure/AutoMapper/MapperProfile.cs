using AutoMapper;
using Domain;

namespace Infrastructure;
public class MapperProfile:Profile
{
    public MapperProfile()
    {
        CreateMap<User, GetUserDto>()
            .ForMember(gu => gu.Status, opt => opt.MapFrom(u => u.Status.ToString()));
    }
}
