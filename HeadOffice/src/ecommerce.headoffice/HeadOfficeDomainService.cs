using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.headoffice.repositories;
using ecommerce.datamodel;
using ecommerce.headoffice.entities;
using ecommerce.core;
using System.Collections.ObjectModel;

namespace ecommerce.headoffice
{
    public class HeadOfficeDomainService
    {
        IHeadOfficeRepository _headOfficeRepository;
        private HeadOffice headOfficeEntity;
        public HeadOfficeDomainService(IHeadOfficeRepository headOfficeRepository)
        {
            _headOfficeRepository = headOfficeRepository;
        }

        #region Public Method
        public HeadOfficeServiceResponse Create(HeadOfficeDomain headOffice)
        {
            HeadOfficeServiceResponse response = new HeadOfficeServiceResponse();


            if (!validateIsNotExist(headOffice.Id))
                response.Messages.Add(new Message("Data is already on database"));
            else
            {
                headOfficeEntity = new HeadOffice();
                MergeExtension.Merge(headOfficeEntity, headOffice);
                _headOfficeRepository.Add(headOfficeEntity);
                _headOfficeRepository.SaveChanges();
            }

            return response;
        }

        public HeadOfficeServiceResponse Update(HeadOfficeDomain headOffice)
        {
            HeadOfficeServiceResponse response = new HeadOfficeServiceResponse();

            if (validateIsNotExist(headOffice.Id))
                response.Messages.Add(new Message("Data is not in Database"));
            else
            {
                headOfficeEntity = new HeadOffice();
                MergeExtension.Merge(headOfficeEntity, headOffice);
                _headOfficeRepository.Update(headOfficeEntity);
                _headOfficeRepository.SaveChanges();
            }
            return response;

        }

        public HeadOfficeServiceResponse GetHeadOfficeById(Guid id)
        {
            HeadOfficeServiceResponse response = new HeadOfficeServiceResponse();
            HeadOffice headOffice = _headOfficeRepository.GetById(id);
            if (headOffice == null)
            {
                response.Messages.Add(new Message("Data is not in Database"));
            }
            else
            {

                HeadOfficeDomain headOfficeDomain = new HeadOfficeDomain();
                MergeExtension.Merge(headOfficeDomain, headOffice);
                response.HeadOfficeDomain = headOfficeDomain;
            }
            return response;
        }

        public HeadOfficeServiceResponse GetAllMerk()
        {
            HeadOfficeServiceResponse response = new HeadOfficeServiceResponse();
            Collection<HeadOffice> headOffices = _headOfficeRepository.GetAll();

            if (headOffices.Count == 0)
            {
                response.Messages.Add(new Message("Tidak Ada Merk Yang Terdaftar"));
            }
            else
            {
                foreach (var m in headOffices)
                {
                    HeadOfficeDomain headOfficeDomain = new HeadOfficeDomain();
                    MergeExtension.Merge(headOfficeDomain, m);
                    response.HeadOfficeDomains.Add(headOfficeDomain);
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
            var x = _headOfficeRepository.GetById(id);
            return (_headOfficeRepository.GetById(id) == null);
        }

        #region Messages
        /// <summary>
        /// HeadOffice Service Message 
        /// </summary>
        public class HeadOfficeServiceResponse : ResponseBase
        {
            public HeadOfficeDomain HeadOfficeDomain { get; set; }

            private Collection<HeadOfficeDomain> _headOfficeDomains;

            public Collection<HeadOfficeDomain> HeadOfficeDomains
            {
                get { return _headOfficeDomains ?? (_headOfficeDomains = new Collection<HeadOfficeDomain>()); }
            }
        }
        #endregion
    }
}
