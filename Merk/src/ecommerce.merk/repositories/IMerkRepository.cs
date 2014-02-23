using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.core;
using ecommerce.merk.aggregate;
using ecommerce.datamodel;

namespace ecommerce.merk.repositories
{
    public interface IMerkRepository : IDALRepository<Merk>
    {
        Merk GetById(string id);
    }
}
