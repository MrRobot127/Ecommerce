using Ecommerce.Shared.Cart;

namespace Ecommerce.Application.MappingProfıles;

public class CartProfile : Profile
{
    public CartProfile()
    {
        CreateMap<CartItem, CartItemDto>().ReverseMap();
    }
}
