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
            Id = Guid.NewGuid(),
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

        [Test]
        public void InsertTest()
        {
            _repository.Add(storeStub1);
            _repository.SaveChanges();
        }



    }
}
