using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
//using Moq;
using ecommerce.merk.repositories;
//using Moq.Language;
//using NUnit.Mocks;
using ecommerce.datamodel;
using NSubstitute;
using ecommerce.merk.aggregate;
using System.Collections.ObjectModel;
using ecommerce.merk.parameters;

namespace ecommerce.merk.integrationtest
{
    public class MerkServiceUnitTest
    {
        #region Dummy Data
        MerkDomainService service;
        
        private Merk merk = new Merk()
        {
            Id = "001",
            Code = "B",
            Name = "Billabong",
        };

        private Merk merkGet = new Merk()
        {
            Id = "002",
            Code = "PS",
            Name = "Planet Surf",
        };

        static CreateParameter paramNull = new CreateParameter("", "", "", "");
        static CreateParameter param = new CreateParameter("005", "AW", "Airwalk", "PT Airwalk");
        static CreateParameter paramExist = new CreateParameter("001", "AW", "Airwalk", "PT Airwalk");
        private MerkDomain merkDomainNull = MerkDomain.Create(paramNull);
        private MerkDomain merkDomain = MerkDomain.Create(param);
        private MerkDomain merkDomainExist = MerkDomain.Create(paramExist);

        private Collection<Merk> merks = new Collection<Merk>();

        #endregion

        #region Test Initialization

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
        public void InsertDataExisttest()
        {
            Assert.AreEqual("Data is already on database", service.Create(merkDomainExist).Messages.FirstOrDefault().Value);
        }

        [Test]
        public void Update()
        {
            Assert.IsEmpty(service.Update(merkDomainExist).Messages);
        }

        [Test]
        public void UpdateNotExistTest()
        {
            Assert.AreEqual("Data is not in Database", service.Update(merkDomain).Messages.FirstOrDefault().Value);
        }

        [Test]
        public void UpdateMerkMandatoryFieldsNullTest()
        {
            Assert.AreEqual(service.Update(merkDomainNull).Messages.FirstOrDefault().Value, "Mandatory fields is empty");
        }

        [Test]
        public void GetMerkById()
        {
            Assert.IsNotNull(service.GetMerkById("001").MerkDomain);
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

        [Test]
        public void 

        #endregion
    }
}
