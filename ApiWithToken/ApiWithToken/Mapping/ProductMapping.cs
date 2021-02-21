using ApiWithToken.Domain.Entities;
using ApiWithToken.Resources;
using AutoMapper;

namespace ApiWithToken.Mapping
{
    public class ProductMapping : Profile
    {
        public ProductMapping()
        {
            CreateMap<ProductResource, Product>();
            CreateMap<Product, ProductResource>();
        
        }
    }
}