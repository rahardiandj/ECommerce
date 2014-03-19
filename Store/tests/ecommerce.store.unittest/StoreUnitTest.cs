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
        #region Dummy Data

        private Store storeStub = new Store()
        {
            Code = "SPT",
            CreateBy = "rahardian",
            CreatedDate = DateTime.Now,
            Id = new Guid("9266e2e6-798d-4910-9fbb-29c4a2a1c4a7"),
            Name = "SportStation",
            Location = "Paris Van Java",
            Type = "Outlet"
        };

        private Store storeUpdateStub = new Store()
        {
            Code = "AF",
            CreateBy = "nastain",
            CreatedDate = DateTime.Now,
            Id = new Guid("182986eb-6846-40f9-a205-e16c19be264e"),
            Name = "AthletesFoot",
            Location = "Cihampelas Walk",
            Type = "Store"
        };

        private Store storeUpdateNullStub = new Store()
        {
            Code = "AFT",
            CreateBy = "anttivierula",
            CreatedDate = DateTime.Now,
            Id = new Guid("560e77ee-17df-492d-b608-fd38076c5b82"),
            Name = "AthletesFoot",
            Location = "Cihampelas Walk",
            Type = "Store"
        };

        #endregion


        #region Test Initialization

        private Store store = null;
        private IObjectContextManager _objectContext;
        private Collection<Store> storeList = null;
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
            //Add first
            _repository.Add(storeStub);
            _repository.SaveChanges();

            //Check whether on database or not
            store = _repository.GetById(storeStub.Id);
            Assert.IsNotNull(store, "Store Berhasil Terbuat");

            //Delete so this method can be used again
            _repository.Delete(storeStub);
            _repository.SaveChanges();
        }

        [Test]
        public void GetByIdStoreTest()
        {
            Store store = _repository.GetById(storeStub.Id);
            Assert.IsNotNull(store);
        }

        [Test]
        [ExpectedException(typeof(System.InvalidOperationException))]
        public void UpdateNullStoreTest()
        {
            _repository.Update(storeUpdateNullStub);
        }

        [Test]
        public void GetAllStoreTest()
        {
            storeList = _repository.GetAll();
            Assert.IsNotNull(storeList, "Store Tidak Kosong");
        }

        [Test]
        public void UpdateTest()
        {
            _repository.Update(storeUpdateStub);
            _repository.SaveChanges();
            store = _repository.GetById(storeUpdateStub.Id);

            Assert.AreEqual(storeUpdateStub.Id, store.Id);
        }

        [Test]
        public void DeleteMerk()
        {
            //Add first to make data dummy
            _repository.Add(storeStub);
            _repository.SaveChanges();

            //Delete
            _repository.Delete(storeStub);
            _repository.SaveChanges();

            //make sure whether data is no longer in database
            store = _repository.GetById(storeStub.Id);
            Assert.IsNull(store, "Store Belum Terhapus");
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
