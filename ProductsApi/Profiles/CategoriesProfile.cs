using AutoMapper;
using ProductsApi.DTOs;
using ProductsApi.Models;

namespace ProductsApi.Profiles;

public class CategoriesProfile : Profile
{
    public CategoriesProfile()
    {
        // Source -> Target
        CreateMap<Category, CategoryReadDTO>();
        CreateMap<CategoryCreateDTO, Category>();
        CreateMap<CategoryUpdateDTO, Category>();
        CreateMap<Category, CategoryDTO>();
    }
}
