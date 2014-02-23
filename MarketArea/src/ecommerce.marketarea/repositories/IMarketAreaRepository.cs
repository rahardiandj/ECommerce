using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using ecommerce.core;

namespace ecommerce.marketarea.repositories
{
    public interface IMarketAreaRepository : IDALRepository<MarketArea>
    {
        MarketArea GetByName(string name);
    }
}
