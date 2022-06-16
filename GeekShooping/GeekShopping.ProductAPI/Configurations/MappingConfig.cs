using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;

namespace GeekShopping.ProductAPI.Configurations
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(configu =>
           {
               configu.CreateMap<ProductVO, Product>();
               configu.CreateMap<Product, ProductVO>();
           });
            return mappingConfig;
        }
    }
}
