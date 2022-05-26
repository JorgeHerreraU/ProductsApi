using AutoMapper;
using NuGet.Packaging;
using ProductsApi.DTOs;
using ProductsApi.Models;

namespace ProductsApi.Profiles;

public class SubcategoriesProfile : Profile
{
    public SubcategoriesProfile()
    {
        // source -> target
        CreateMap<Subcategory, SubcategoryReadDTO>();
        CreateMap<SubcategoryCreateDTO, Subcategory>();
        CreateMap<SubcategoryUpdateDTO, Subcategory>();
        CreateMap<Subcategory, SubcategoryDTO>();
    }
}
