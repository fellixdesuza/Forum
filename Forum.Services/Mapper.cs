
using AutoMapper;
using Forum.Entities;
using Forum.Models;
using Forum.Models.Identity;
using Microsoft.AspNetCore.Identity;

namespace Forum.Services
{
    public static class Mapper
    {
        public static IMapper Initializer()
        {
            MapperConfiguration configuration = new(config =>
            {
                config.CreateMap<Post, PostForAddDto>().ReverseMap();
                config.CreateMap<Post, PostForGetDto>().ReverseMap();
                config.CreateMap<Post, PostForUpdateDto>().ReverseMap();

                config.CreateMap<Comment, CommentForAddDto>().ReverseMap();
                config.CreateMap<Comment, CommentForGetDto>().ReverseMap();
                config.CreateMap<Comment, CommentForUpdateDto>().ReverseMap();

                config.CreateMap<UserDto, IdentityUser>().ReverseMap();
                config.CreateMap<RegistrationRequestDto, IdentityUser>().
                ForMember(destination => destination.UserName, options => options.MapFrom(source => source.Email)).
                ForMember(destination => destination.NormalizedUserName, options => options.MapFrom(source => source.Email.ToUpper())).
                ForMember(destination => destination.Email, options => options.MapFrom(source => source.Email)).
                ForMember(destination => destination.NormalizedEmail, options => options.MapFrom(source => source.Email.ToUpper()));

            });

            return configuration.CreateMapper();
         }
    }
}
