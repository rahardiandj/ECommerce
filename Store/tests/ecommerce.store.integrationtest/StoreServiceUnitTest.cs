using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ecommerce.datamodel;
using NUnit.Framework;
using NSubstitute;
using ecommerce.store.repositories;
using System.Collections.ObjectModel;
using ecommerce.store.entities;

namespace ecommerce.store.integrationtest
{
    public class StoreServiceUnitTest
    {
        #region Dummy Data

        StoreDomain s = null;

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

        private Store store2 = new Store()
        {
            Code = "AF",
            CreateBy = "WhiteNoise",
            CreatedDate = DateTime.Now,
            Id = new Guid("e41118af-a430-407e-a472-4db2750086b9"),
            Location = "Trans Studio Bandung",
            Name = "Athlete's Foot",
            Type = "Outlet",
        };

        private Store storeInsert = new Store()
        {
            Code = "JF",
            Id = new Guid("fd29a909-5019-4769-a1b5-9462f2bb6706"),
            Location = "Trans Studio Bandung",
            Name = "Jeffry Store",
            Type = "Toko",
        };

        private StoreDomain storeDomainNull = new StoreDomain()
        {
            Code = "",
            Id = new Guid("54bb1e9e-804c-4b1d-9799-1d18e705c976"),
            Location = "",
            Name = "",
            Type = "",
        };

        private StoreDomain storeDomain = new StoreDomain()
        {
            Code = "RK",
            Id = new Guid("fd29a909-5019-4769-a1b5-9462f2bb6706"),
            Location = "Balubur Town Square",
            Name = "Ridwan Kamil",
            Type = "Toko",
        };

        private StoreDomain storeDomainInsert = new StoreDomain()
        {
            Code = "NN",
            Id = new Guid("90fc798e-37c5-4d1b-8563-af084b227eca"),
            Location = "Paris Van Java",
            Name = "Nastain Heritage",
            Type = "Outlet Resmi",
        };

        private StoreDomain storeDomainExist = new StoreDomain()
        {
            Code = "DJ",
            Id = new Guid("fd29a909-5019-4769-a1b5-9462f2bb6706"),
            Location = "Bandung Indah Plaza",
            Name = "Dwi Juliarhouse",
            Type = "i-Store",
        };

        private Collection<Store> stores = new Collection<Store>();

        private Guid id1 = new Guid("fd29a909-5019-4769-a1b5-9462f2bb6706");

        #endregion

        #region Test Initilization

        private StoreDomainService service;
        
        [SetUp]
        public void Initialization()
        {
            //Mock up object
            var repository = Substitute.For<IStoreRepository>();
            s = new StoreDomain();
            service = new StoreDomainService(repository);
            repository.GetById(id1).Returns(store1);
            stores.Add(store1);
            stores.Add(storeInsert);
            repository.GetAll().Returns(stores);
        }

        #endregion

        #region Test Method

        [Test]
        public void GetByIdStoreServiceTest()
        {
            Assert.NotNull(service.GetStoreById(id1));
        }

        public void GetStoreIfNotExistTest()
        {
            string message = service.GetStoreById(store2.Id).Messages.FirstOrDefault().ToString();
            Assert.AreEqual(message, "Data is not in Database");
        }

        [Test]
        public void InsertTest()
        {
            Assert.IsNotEmpty(service.Create(storeDomain).Messages);
        }

        [Test]
        public void InsertNotNullTest()
        {
            Assert.IsEmpty(service.Create(storeDomainInsert).Messages);
        }

        [Test]
        public void UpdateTest()
        {
            Assert.IsEmpty(service.Update(storeDomainExist).Messages);
        }

        [Test]
        public void UpdateNotExistTest()
        {
            Assert.AreEqual(service.Update(storeDomainInsert).Messages.FirstOrDefault().Value, "Data is not in Database");
        }

        [Test]
        public void GetAllStoreTest()
        {
            Collection<StoreDomain> headOffices = service.GetAllStore().StoreDomains;
            Assert.IsNotNull(stores);
        }

        [Test]
        public void GetAllStoreNullTest()
        {
            var _repository = Substitute.For<IStoreRepository>();
            _repository.GetAll().Returns(new Collection<Store>());
            StoreDomainService _service = new StoreDomainService(_repository);
            string message = _service.GetAllStore().Messages.FirstOrDefault().Value;
            Assert.AreEqual(message, "Tidak Ada Store Yang Terdaftar");
        }

        [Test]
        public void getValidateIsNotExistTest()
        {
            Assert.IsNotNull(service.GetStoreById(store1.Id));
        }

        #endregion
    }
}
