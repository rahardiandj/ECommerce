using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ecommerce.merk.exceptions
{
    public class CodeBrandAlreadyExistException : Exception
    {
        public CodeBrandAlreadyExistException(string code)
            : base(string.Format("Code Brand {0} already exist", code))
        {
        }
    }
}
