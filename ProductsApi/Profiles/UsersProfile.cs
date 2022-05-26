using AutoMapper;
using ProductsApi.DTOs;
using ProductsApi.Models;

namespace ProductsApi.Profiles;

public class UsersProfile : Profile
{
    public UsersProfile()
    {
        // source -> target
        CreateMap<User, UserReadDTO>();
        CreateMap<UserCreateDTO, User>();
        CreateMap<UserUpdateDTO, User>();
        CreateMap<User, UserUpdateDTO>();
        CreateMap<UserRegisterDTO, User>();
    }
}