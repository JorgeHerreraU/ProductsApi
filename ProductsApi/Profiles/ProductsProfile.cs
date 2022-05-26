using AutoMapper;
using ProductsApi.DTOs;
using ProductsApi.Models;

namespace ProductsApi.Profiles;

public class ProductsProfile: Profile
{
    public ProductsProfile()
    {
        // source -> target
        CreateMap<Product, ProductReadDTO>();
        CreateMap<ProductCreateDTO, Product>();
        CreateMap<ProductUpdateDTO, Product>();
    }
}
