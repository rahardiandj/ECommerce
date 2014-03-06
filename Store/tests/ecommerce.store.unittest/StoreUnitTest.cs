using System;
using ecommerce.repository.test;
using ecommerce.core;
using ecommerce.store.repositories;
using Microsoft.Practices.ServiceLocation;
using ecommerce.store.repository;
using NUnit.Framework;
using ecommerce.datamodel;
using System.Collections.ObjectModel;

namespace ecommerce.store.unittest
{
    public class StoreUnitTest : RepositoryBaseTest
    {
        private Store store = null;
        private IObjectContextManager _objectContext;
        private IStoreRepository _repository;
        private Collection<Store> storeList = null;

        #region Dummy Data
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

        private Store storeUpdateStub1 = new Store()
        {
            Code = "TNA",
            CreateBy = "nastain",
            CreatedDate = DateTime.Now,
            Id = new Guid("9266e2e6-798d-4910-9fbb-29c4a2a1c4a7"),
            Name = "Adidas Official Store",
            Location = "Cihampelas Walk",
            Type = "Official Store"
        };

        #endregion

        #region Test Initialization

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

            //Check whether on database or not
            store = _repository.GetById(storeStub1.Id);
            Assert.IsNotNull(store, "Data Store Baru Berhasil Terbuat");

            //Delete so this method can be used again
            _repository.Delete(storeStub1);
            _repository.SaveChanges();
        }

        [Test]
        public void GetByIdstoreTest()
        {
            Store store = _repository.GetById(storeStub1.Id);
            Assert.IsNull(store);
        }
      
        [Test]
        public void AddstoreTest()
        {
            //Add first
            _repository.Add(storeStub1);
            _repository.SaveChanges();

            //Check whether on database or not
            store = _repository.GetById(storeStub1.Id);
            Assert.IsNotNull(store, "Data Head Office Baru Berhasil Terbuat");

            //Delete so this method can be used again
            _repository.Delete(storeStub1);
            _repository.SaveChanges();
        }

        [Test]
        public void DeletestoreTest()
        {
            //Add first to make data dummy
            _repository.Add(storeStub1);
            _repository.SaveChanges();

            //Delete
            _repository.Delete(storeStub1);
            _repository.SaveChanges();

            //make sure wheter data is no longer in database
            store = _repository.GetById(storeStub1.Id);
            Assert.IsNull(store, "Data Store Terhapus");
        }

        [Test]
        public void GetAllstoreTest()
        {
            storeList = _repository.GetAll();
            Assert.IsNotNull(storeList, "Data Store tidak Kosong");
        }


        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void UpdateNullstoreTest()
        {
            _repository.Update(storeUpdateStub1);
            _repository.SaveChanges();
            store = _repository.GetById(storeUpdateStub1.Id);
        }

        
        #endregion
    }
}
