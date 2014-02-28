using System;
using ecommerce.repository.test;
using ecommerce.core;
using ecommerce.store.repositories;
using Microsoft.Practices.ServiceLocation;
using ecommerce.store.repository;
using NUnit.Framework;
using ecommerce.datamodel;

namespace ecommerce.store.unittest
{
    public class StoreUnitTest : RepositoryBaseTest
    {
        private Store storeStub1 = new Store()
        {
            Code = "SPT",
            CreateBy = "rahardian",
            CreatedDate = DateTime.Now,
            Id = new Guid("9266e2e6-798d-4910-9fbb-29c4a2a1c4a7"),
            Name = "SportStation",
            Location = "Paris Van Java",
            Type = "Outlet"
        };


        #region Test Initialization

        private IObjectContextManager _objectContext;
        private IStoreRepository _repository;

        [SetUp]
        public void Initialization()
        {
            _objectContext = ServiceLocator.Current.GetInstance<IObjectContextManager>("ObjectContextManager");
            _repository = new StoreRepository(_objectContext);
        }

        #endregion

        #region Test Method

        [Test]
        public void InsertTest()
        {
            _repository.Add(storeStub1);
            _repository.SaveChanges();
        }

        [Test]
        public void GetByIdStoreTest()
        {
            Store store = _repository.GetById(storeStub1.Id);
            Assert.IsNotNull(store);
        }


        [Test]
        public void KaliStoreTest()
        {
            int c = _repository.Kali(5, 2);
            Assert.AreEqual(c, 10);
        }
        #endregion

    }
}
