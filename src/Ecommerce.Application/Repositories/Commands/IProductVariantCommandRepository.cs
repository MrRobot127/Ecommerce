using Ecommerce.Shared.Product;

namespace Ecommerce.Application.Repositories.Commands;

public interface IProductVariantCommandRepository : ICommandRepository<ProductVariant, int>
{
}
