using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using NUnit.Framework;
using NSubstitute;
using ecommerce.store.repositories;
using System.Collections.ObjectModel;

namespace ecommerce.store.integrationtest
{
    public class StoreServiceUnitTest
    {
        #region Dummy Data

        private StoreDomainService service;

        private Store store1 = new Store()
        {
            Code = "SS",
            CreateBy = "BlackMail",
            CreatedDate = DateTime.Now,
            Id = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A13"),
            Location = "Matahari BIP",
            Name = "Sport Station",
            Type = "Toko",
        };

        private Collection<Store> stores = new Collection<Store>();

        private Guid id1 = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A13");

        #endregion

        #region Test Initilization
        
        [SetUp]
        public void Initialization()
        {
            //Mock up object
            var repository = Substitute.For<IStoreRepository>();
            service = new StoreDomainService(repository);
            repository.GetById(id1).Returns(store1);
            stores.Add(store1);
            repository.GetAll().Returns(stores);
        }

        #endregion

        [Test]
        public void GetByIdStoreServiceTest()
        {
            Assert.NotNull(service.GetStoreById(id1));
        }
    }
}
