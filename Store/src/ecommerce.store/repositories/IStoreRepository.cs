using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using ecommerce.core;

namespace ecommerce.store.repositories
{
    public interface IStoreRepository : IDALRepository<Store>
    {
        Store GetById(Guid id);
    }
}
