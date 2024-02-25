using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities;

public class ProductType : BaseEntity<int>
{
    public string Name { get; set; } = string.Empty;

}
