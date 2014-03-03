using ecommerce.merk.aggregate;
using ecommerce.merk.entities;
using ecommerce.merk.parameters;
using ecommerce.merk.exceptions;
using ecommerce.merk.repositories;
using ecommerce.core;
using ecommerce.datamodel;
using System.Collections.ObjectModel;

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

        #region Public Method
        public MerkServiceResponse Create(MerkDomain merk)
        {
            MerkServiceResponse response = new MerkServiceResponse();
           
            
            if (!validateIsNotExist(merk.Id.OwnerId))
                response.Messages.Add(new Message("Data is already on database"));
            else
            {
                MergeExtension.Merge(merkEntity, merk);
                _merkRepository.Add(merkEntity);
                _merkRepository.SaveChanges();
            }

            return response;
        }

        public MerkServiceResponse Update(MerkDomain merk)
        {
            MerkServiceResponse response = new MerkServiceResponse();

            if (!validateIsNotExist(merk.Id.OwnerId))
                response.Messages.Add(new Message("Data is not in Database"));
            else
            {
                MergeExtension.Merge(merkEntity, merk);

                _merkRepository.Update(merkEntity);
                _merkRepository.SaveChanges();
            }
            return response;

        }

        public MerkServiceResponse GetMerkById(string id)
        {
            MerkServiceResponse response = new MerkServiceResponse();

            if (!validateIsNotExist(id))
            {
                response.Messages.Add(new Message("Data is not in Database"));
            }
            else
            {
                Merk merk = _merkRepository.GetById(id);
                CreateParameter param = new CreateParameter(merk.Id, merk.Code, merk.Name,merk.Manufacture);
                response.MerkDomain = MerkDomain.Create(param);
            }
            return response;
        }

        public MerkServiceResponse GetAllMerk()
        {
            MerkServiceResponse response = new MerkServiceResponse();
            Collection<Merk> merks = _merkRepository.GetAll();

            if (merks.Count == 0)
            {
                response.Messages.Add(new Message("Tidak Ada Merk Yang Terdaftar"));
            }
            else
            {
                foreach (var m in merks)
                {
                    CreateParameter param = new CreateParameter(m.Id, m.Code, m.Name, m.Manufacture);
                    MerkDomain merk = MerkDomain.Create(param);
                    MergeExtension.Merge(merk, m);
                    response.MerkDomains.Add(merk);
                }
            }
            return response;
        }

        public void Delete(string ownerId, string code)
        {
            //MerkId id = new MerkId(ownerId, code);
            //brandRepository.Remove(id);
        }
        #endregion

        private bool validateIsNotExist(string id)
        {
            Merk merk = _merkRepository.GetById(id);
            return merk == null;
        }
    }
        
    #region Messages
    /// <summary>
    /// Merk Service Message 
    /// </summary>
    public class MerkServiceResponse : ResponseBase
    {
        public MerkDomain MerkDomain { get; set; }

        private Collection<MerkDomain> _merkDomains;

        public Collection<MerkDomain> MerkDomains
        {
            get { return _merkDomains ?? (_merkDomains = new Collection<MerkDomain>()); }
        }
    }
    #endregion
}
