using ecommerce.merk;

namespace ecommerce.merk.appservices
{
    public class MerkService
    {
        private MerkDomainService domainService;
        public MerkService(MerkDomainService domainService)
        {
            this.domainService = domainService;
        }

        //public string Create(string ownerId, string code, string name)
        //{
        //    var result = domainService.Create(ownerId, code, name);
        //    return result.Id.Code;
        //}

        //public void ChangeName(string ownerId, string code, string name)
        //{
        //    domainService.ChangeName(ownerId, code, name);
        //}

        public void Delete(string ownerId, string code)
        {
            domainService.Delete(ownerId, code);
        }
    }
}
