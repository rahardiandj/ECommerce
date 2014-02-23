
using ecommerce.merk.entities;
using ecommerce.merk.parameters;
namespace ecommerce.merk.aggregate
{
    public class MerkDomain
    {
        public MerkId Id { get; private set; }
        public string Name { get; private set; }

        public static MerkDomain Create(CreateParameter param)
        {
            return new MerkDomain(param);
        }

        public void ChangeName(string name)
        {
            this.Name = name;
        }

        private MerkDomain(CreateParameter param)
        {
            this.Id = new MerkId(param.OwnerId, param.Code);
            this.Name = param.Name;
        }
    }
}
