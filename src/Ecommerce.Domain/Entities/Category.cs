using Ecommerce.Domain.Common;

namespace Ecommerce.Domain.Entities;

public class Category : BaseAuditableEntity<int>
{
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
}
