using AutoMapper;
using BM7_Backend.Dto;
using BM7_Backend.Models;

namespace BM7_Backend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<NewUserDto, User>();
    }
}