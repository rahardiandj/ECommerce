
namespace ecommerce.merk.entities
{
    public class MerkId
    {
        public string OwnerId { get; private set; }
        public string Code { get; private set; }

        public MerkId(string ownerId, string code)
        {
            this.OwnerId = ownerId;
            this.Code = code;
        }
    }
}
