using ecommerce.merk.aggregate;
using ecommerce.merk.entities;
using ecommerce.merk.parameters;
using ecommerce.merk.exceptions;
using ecommerce.merk.repositories;
using ecommerce.core;
using ecommerce.datamodel;

namespace ecommerce.merk
{
    public class MerkDomainService
    {
        IMerkRepository _merkRepository;
        private Merk merkEntity;
        public MerkDomainService(IMerkRepository merkRepository)
        {
            _merkRepository = merkRepository;
        }

        public MerkDomain Create(string id, string code, string name)
        {
           CreateParameter param = new CreateParameter(id, code, name);
           AssertCodeNotExist(id, code);
           var merk = MerkDomain.Create(param);
           MergeExtension.Merge(merkEntity, merk);
           _merkRepository.Add(merkEntity);
           return merk;
        }

        public void ChangeName(string ownerId, string code, string name)
        {
            MerkId id = new MerkId(ownerId, code);
            //var merk = _merkRepository.GetById(;
            //brand.ChangeName(name);
            //brandRepository.Update(brand);
        }

        public MerkDomain GetMerkById(string id)
        {
            Merk merk = _merkRepository.GetById(id);
            CreateParameter param = new CreateParameter(merk.Id, merk.Code, merk.Name);
            MerkDomain merkDomain = MerkDomain.Create(param);
            //MergeExtension.Merge(merkDomain, merk);
            return merkDomain;
        }

        private void AssertCodeNotExist(string ownerId, string code)
        {
            //MerkId id = new MerkId(ownerId, code);
            //if (brandRepository.IsExist(id))
            //    throw new CodeBrandAlreadyExistException(code);
        }

        public void Delete(string ownerId, string code)
        {
            //MerkId id = new MerkId(ownerId, code);
            //brandRepository.Remove(id);
        }
    }
}
