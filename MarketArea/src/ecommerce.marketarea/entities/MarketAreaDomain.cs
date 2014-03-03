using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecommerce.marketarea.entities
{
    public class MarketAreaDomain
    {
        public MarketAreaDomain()
        {

        }

        public Guid Id { get; set; }
        public string CodeArea { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
