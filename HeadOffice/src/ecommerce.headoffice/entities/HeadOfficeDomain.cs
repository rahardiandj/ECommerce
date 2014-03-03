﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecommerce.headoffice.entities
{
    public class HeadOfficeDomain
    {
        public HeadOfficeDomain()
        {

        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
