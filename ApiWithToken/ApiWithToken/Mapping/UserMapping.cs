using ApiWithToken.Domain.Entities;
using ApiWithToken.Resources;
using AutoMapper;

namespace ApiWithToken.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserResource, User>();
            CreateMap<User, UserResource>();
        }

    }
}
