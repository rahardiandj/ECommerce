using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecommerce.store.entities
{
    public class StoreDomain
    {
        public StoreDomain()
        {

        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Location { get; set; }
        public string Type { get; set; }
    }
}
