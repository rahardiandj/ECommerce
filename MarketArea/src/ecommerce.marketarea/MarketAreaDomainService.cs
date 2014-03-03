using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.marketarea.repositories;
using ecommerce.datamodel;
using ecommerce.core;
using ecommerce.marketarea.entities;
using System.Collections.ObjectModel;

namespace ecommerce.marketarea
{
    public class MarketAreaDomainService
    {
        IMarketAreaRepository _marketAreaRepository;
        private MarketArea marketAreaEntity;
        public MarketAreaDomainService(IMarketAreaRepository marketAreaRepository)
        {
            _marketAreaRepository = marketAreaRepository;
        }

        #region Public Method
        public MarketAreaServiceResponse Create(MarketAreaDomain marketArea)
        {
            MarketAreaServiceResponse response = new MarketAreaServiceResponse();


            if (!validateIsNotExist(marketArea.Id))
                response.Messages.Add(new Message("Data is already on database"));
            else
            {
                MergeExtension.Merge(marketAreaEntity, marketArea);
                _marketAreaRepository.Add(marketAreaEntity);
                _marketAreaRepository.SaveChanges();
            }

            return response;
        }

        public MarketAreaServiceResponse Update(MarketAreaDomain marketArea)
        {
            MarketAreaServiceResponse response = new MarketAreaServiceResponse();

            if (!validateIsNotExist(marketArea.Id))
                response.Messages.Add(new Message("Data is not in Database"));
            else
            {
                MergeExtension.Merge(marketAreaEntity, marketArea);
                _marketAreaRepository.Update(marketAreaEntity);
                _marketAreaRepository.SaveChanges();
            }
            return response;

        }

        public MarketAreaServiceResponse GetMarketAreaById(Guid id)
        {
            MarketAreaServiceResponse response = new MarketAreaServiceResponse();
            MarketArea marketArea = _marketAreaRepository.GetById(id);
            if (marketArea == null)
            {
                response.Messages.Add(new Message("Data is not in Database"));
            }
            else
            {
                
                MarketAreaDomain marketAreaDomain = new MarketAreaDomain();
                MergeExtension.Merge(marketAreaDomain, marketArea);
                response.MarketAreaDomain = marketAreaDomain;
            }
            return response;
        }

        public MarketAreaServiceResponse GetAllMerk()
        {
            MarketAreaServiceResponse response = new MarketAreaServiceResponse();
            Collection<MarketArea> marketAreas = _marketAreaRepository.GetAll();

            if (marketAreas.Count == 0)
            {
                response.Messages.Add(new Message("Tidak Ada Merk Yang Terdaftar"));
            }
            else
            {
                foreach (var m in marketAreas)
                {
                    MarketAreaDomain marketAreaDomain = new MarketAreaDomain();
                    MergeExtension.Merge(marketAreaDomain, m);
                    response.MarketAreaDomains.Add(marketAreaDomain);
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
            return (_marketAreaRepository.GetById(id) == null);
        }

        #region Messages
        /// <summary>
        /// MarketArea Service Message 
        /// </summary>
        public class MarketAreaServiceResponse : ResponseBase
        {
            public MarketAreaDomain MarketAreaDomain { get; set; }

            private Collection<MarketAreaDomain> _marketAreaDomains;

            public Collection<MarketAreaDomain> MarketAreaDomains
            {
                get { return _marketAreaDomains ?? (_marketAreaDomains = new Collection<MarketAreaDomain>()); }
            }
        }
        #endregion
    }
}
