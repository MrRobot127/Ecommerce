using Ecommerce.Shared.Product;

namespace Ecommerce.Application.Repositories.Commands;

public interface IProductCommandRepository : ICommandRepository<Product, int>
{
}
