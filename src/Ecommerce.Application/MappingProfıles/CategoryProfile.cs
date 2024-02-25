using Ecommerce.Shared.Category;

namespace Ecommerce.Application.MappingProfıles;

public class CategoryProfile : Profile
{
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
    }
}
