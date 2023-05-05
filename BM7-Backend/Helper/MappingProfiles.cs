using AutoMapper;
using BM7_Backend.Dto;
using BM7_Backend.Models;
using Transaction = System.Transactions.Transaction;

namespace BM7_Backend.Helper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<User, UserDto>();
        CreateMap<NewUserDto, User>();
        CreateMap<NewTransactionDto, Transaction>();
        CreateMap<Transaction, NewTransactionDto>();
        CreateMap<NewCategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
    }
}