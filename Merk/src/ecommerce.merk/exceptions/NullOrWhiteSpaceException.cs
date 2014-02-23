using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecommerce.merk.exceptions
{
    public class NullOrWhiteSpaceException : Exception
    {
        public NullOrWhiteSpaceException(string propertyName)
            : base(string.Format("Field {0} can't string empty", propertyName))
        {
        }
    }
}
