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

namespace ecommerce.merk.integrationtest
{
    public class MerkIntegrationTest
    {
        //private DynamicMock merkRepositoryMock;

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

        private Collection<Merk> merks = new Collection<Merk>();

        #endregion
        
        [SetUp]
        public void Initialization()
        {
            var repository = Substitute.For<IMerkRepository>();
            service = new MerkDomainService(repository);
            merks.Add(merk);
            merks.Add(merkGet);
            repository.GetById("001").Returns(merk);
            repository.GetById("002").Returns(new Merk());
            repository.GetAll().Returns(merks);
        }

        [Test]
        public void Insert()
        {

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
    }
}
