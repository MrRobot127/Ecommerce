using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities;

public class Image : BaseEntity<int>
{
    public string Data { get; set; } = string.Empty;
}
