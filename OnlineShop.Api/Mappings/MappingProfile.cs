using AutoMapper;
using OnlineShop.Core.Models;
using OnlineShop.DTOs;

namespace OnlineShop.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CategoryDto, Category>();

        CreateMap<Category, CategoryDto>();
    }
}