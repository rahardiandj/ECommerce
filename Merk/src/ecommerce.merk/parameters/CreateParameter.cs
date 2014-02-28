using System;
using ecommerce.merk.exceptions;

namespace ecommerce.merk.parameters
{
    public class CreateParameter
    {
        public string OwnerId { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
        public string Manufacture { get; private set; }

        public CreateParameter(string ownerId, string code, string name, string manufacture)
        {
            if (String.IsNullOrWhiteSpace(code))
                throw new NullOrWhiteSpaceException("Code Brand");
            else if (String.IsNullOrWhiteSpace(name))
                throw new NullOrWhiteSpaceException("Brand Name");

            this.OwnerId = ownerId;
            this.Code = code;
            this.Name = name;
            this.Manufacture = manufacture;
        }
    }
}
