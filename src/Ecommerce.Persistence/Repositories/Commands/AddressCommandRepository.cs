﻿namespace Ecommerce.Persistence.Repositories.Commands
{
    public class AddressCommandRepository : CommandRepository<Address, int>, IAddressCommandRepository
    {
        public AddressCommandRepository(PersistenceDataContext context) : base(context)
        {
        }
    }
}
