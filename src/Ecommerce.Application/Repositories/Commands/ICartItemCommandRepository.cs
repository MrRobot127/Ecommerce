using Ecommerce.Shared.Cart;

namespace Ecommerce.Application.Repositories.Commands;

public interface ICartItemCommandRepository : ICommandRepository<CartItem, int>
{
}
