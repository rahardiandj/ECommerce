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

namespace ecommerce.merk.integrationtest
{
    public class MerkIntegrationTest
    {
        //private DynamicMock merkRepositoryMock;
        MerkDomainService service;
        
        private Merk merk = new Merk()
        {
            Id = "001",
            Code = "B",
            Name = "Billabong",
        };

        [SetUp]
        public void Initialization()
        {
            var repository = Substitute.For<IMerkRepository>();
            service = new MerkDomainService(repository);
            repository.GetById("001").Returns(merk);
            repository.GetById("002").Returns(new Merk());
            //repository.Add(Arg.Is<Merk>(merk));
            //merkRepositoryMock = new DynamicMock(typeof(IMerkRepository));
            //merkRepositoryMock.SetReturnValue("GetById", merk);
            
            //service = new MerkDomainService((IMerkRepository)merkRepositoryMock.MockInstance);
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
    }
}
