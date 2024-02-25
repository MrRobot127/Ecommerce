namespace Ecommerce.Application.MappingProfıles;

public class AddressProfile : Profile
{
    public AddressProfile()
    {
        CreateMap<Address, AddressDto>().ReverseMap();
    }
}
