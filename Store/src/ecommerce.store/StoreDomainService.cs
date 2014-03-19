using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.store.repositories;
using ecommerce.datamodel;
using ecommerce.store.entities;
using System.Collections.ObjectModel;
using ecommerce.core;

namespace ecommerce.store
{
    public class StoreDomainService
    {
        IStoreRepository _storeRepository;
        private Store storeEntity;
        public StoreDomainService(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }

        #region Public Method
        public StoreServiceResponse Create(StoreDomain store)
        {
            StoreServiceResponse response = new StoreServiceResponse();


            if (!validateIsNotExist(store.Id))
                response.Messages.Add(new Message("Data is already on database"));
            else
            {
                storeEntity = new Store();
                MergeExtension.Merge(storeEntity, store);
                _storeRepository.Add(storeEntity);
                _storeRepository.SaveChanges();
            }

            return response;
        }

        public StoreServiceResponse Update(StoreDomain store)
        {
            StoreServiceResponse response = new StoreServiceResponse();

            if (!validateIsNotExist(store.Id))
                response.Messages.Add(new Message("Data is not in Database"));
            else
            {
                storeEntity = new Store();
                MergeExtension.Merge(storeEntity, store);
                _storeRepository.Update(storeEntity);
                _storeRepository.SaveChanges();
            }
            return response;

        }

        public StoreServiceResponse GetStoreById(Guid id)
        {
            StoreServiceResponse response = new StoreServiceResponse();
            Store store = _storeRepository.GetById(id);
            if (store == null)
            {
                response.Messages.Add(new Message("Data is not in Database"));
            }
            else
            {
                
                StoreDomain storeDomain = new StoreDomain();
                MergeExtension.Merge(storeDomain, store);
                response.StoreDomain = storeDomain;
            }
            return response;
        }

        public StoreServiceResponse GetAllStore()
        {
            StoreServiceResponse response = new StoreServiceResponse();
            Collection<Store> stores = _storeRepository.GetAll();

            if (stores.Count == 0)
            {
                response.Messages.Add(new Message("Tidak Ada Store Yang Terdaftar"));
            }
            else
            {
                foreach (var m in stores)
                {
                    StoreDomain storeDomain = new StoreDomain();
                    MergeExtension.Merge(storeDomain, m);
                    response.StoreDomains.Add(storeDomain);
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

        private bool validateIsNotExist(Guid id)
        {
            return (_storeRepository.GetById(id) == null);
        }

        #region Messages
        /// <summary>
        /// Store Service Message 
        /// </summary>
        public class StoreServiceResponse : ResponseBase
        {
            public StoreDomain StoreDomain { get; set; }

            private Collection<StoreDomain> _storeDomains;

            public Collection<StoreDomain> StoreDomains
            {
                get { return _storeDomains ?? (_storeDomains = new Collection<StoreDomain>()); }
            }
        }
        #endregion
    }
}
