namespace Ecommerce.Application.MappingProfıles;

public class ProductTypeProfile : Profile
{
    public ProductTypeProfile()
    {
        CreateMap<ProductType, ProductTypeDto>().ReverseMap();
    }
}
