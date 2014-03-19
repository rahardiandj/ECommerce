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

        StoreDomainService service;

        private Store store = new Store()
        {
            Code = "SS",
            CreateBy = "BlackMail",
            CreatedDate = DateTime.Now,
            Id = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A13"),
            Location = "Matahari BIP",
            Name = "Sport Station",
            Type = "Toko",
        };

        private Store storeGet = new Store()
        {
            Code = "AF",
            CreateBy = "WhiteNoise",
            CreatedDate = DateTime.Now,
            Id = new Guid("e41118af-a430-407e-a472-4db2750086b9"),
            Location = "Trans Studio Mall",
            Name = "Athlete Foot",
            Type = "Counter",
        };

        private Collection<Store> stores = new Collection<Store>();

        private Guid id1 = new Guid("4CC4B154-0F16-49E0-AB9B-8D17354C4A13");

        #endregion

        #region Test Initilization
        
        [SetUp]
        [SetUp]
        public void Initialization()
        {
            //Mock up object
            var repository = Substitute.For<IMerkRepository>();
            service = new MerkDomainService(repository);
            merks.Add(merk);
            merks.Add(merkGet);
            repository.GetById("001").Returns(merk);
            repository.GetById("002").Returns(new Merk());
            repository.GetAll().Returns(merks);
        }

        #endregion

        #region Test Method

        [Test]
        public void Insert()
        {
            Assert.IsEmpty(service.Create(merkDomain).Messages);
        }

        [Test]
        public void Update()
        {
            Assert.IsEmpty(service.Update(merkDomainExist).Messages);
        }

        [Test]
        public void UpdateMerkMandatoryFieldsNullTest()
        {
            Assert.AreEqual(service.Update(merkDomainNull).Messages.FirstOrDefault().Value, "Mandatory fields is empty");
        }

        [Test]
        public void GetMerkById()
        {
            Assert.IsNotNull(service.GetMerkById("001"));
        }
        [Test]
        public void GetMerkIfNotExist()
        {
            string message = service.GetMerkById("002").Messages.FirstOrDefault().ToString();
            Assert.AreEqual(message, "Data is not in Database");
        }

        [Test]
        public void GetAllMerkTest()
        {
            Collection<MerkDomain> merks = service.GetAllMerk().MerkDomains;
            Assert.IsNotNull(merks);
        }

        [Test]
        public void GetAllMerkNullTest()
        {
            var _repository = Substitute.For<IMerkRepository>();
            _repository.GetAll().Returns(new Collection<Merk>());
            MerkDomainService _service = new MerkDomainService(_repository);
            string message = _service.GetAllMerk().Messages.FirstOrDefault().Value;
            Assert.AreEqual(message, "Tidak Ada Merk Yang Terdaftar");
        }
        [Test]
        public void InsertMerkMandatoryFieldNullTest()
        {
            string message = service.Create(merkDomainNull).Messages.FirstOrDefault().Value;

            Assert.AreEqual(message, "Mandatory fields is empty");
        }
    }
}
